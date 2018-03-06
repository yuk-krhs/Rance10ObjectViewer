using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rance10ObjectViewer
{
    public class Rance10ObjectAnalyzer
    {
        public uint                     ObjectManagerAddress    { get; private set; }
        public int                      ObjectCount             { get; private set; }
        public int                      TableCount              { get; private set; }
        public uint                     TableStart              { get; private set; }
        public uint                     TableEnd                { get; private set; }
        public uint[]                   TableAddresses          { get; private set; }
        public uint[]                   ObjectAddresses         { get; private set; }
        public Rance10Object[]          Objects                 { get; private set; }
        public Dictionary<int, Rance10Object>[] ObjectsByType   { get; private set; }
        public Dictionary<string, List<Rance10Object>>
                                        ClassObject             { get; private set; }
        public StreamWriter             Log                     { get; private set; }

        public void Analyze(Rance10 rance10)
        {
            Begin();
            rance10.Suspend();

            try
            {
                ReadTables(rance10);
                ReadObjects(rance10);
                SortObjects(rance10);
                AnalyzeObjects(rance10);
            } finally
            {
                rance10.Resume();
                End();
            }
        }

        public Rance10Object GetObject(int seq)
        {
            if(seq <= 0 || seq >= Objects.Length)
                return null;

            return Objects[seq];
        }

        private void Begin()
        {
            var dir     = Path.GetDirectoryName(GetType().Assembly.Location);
            var logfile = Path.Combine(dir, "log.txt");
            Log         = new StreamWriter(logfile, false, Encoding.Default);
        }

        private void End()
        {
            if(null != Log)
            {
                try { Log.Flush();   } catch {}
                try { Log.Close();   } catch {}
                try { Log.Dispose(); } catch {}

                Log = null;
            }
        }

        private void ReadTables(Rance10 rance10)
        {
            var buf = new byte[256*1000];
            var m   = rance10.ReadMemory(new IntPtr(0x007FCBB0), buf, 8);

            if(!m.Success || m.ReadedSize != 8)
                return;

            var mng1= BitConverter.ToUInt32(m.Data, 0);
            var mng2= BitConverter.ToUInt32(m.Data, 4);
            m       = rance10.ReadMemory(new IntPtr(mng1), buf, 32);

            if(!m.Success || m.ReadedSize != 32)
                return;

            var tbl = BitConverter.ToUInt32(m.Data,  8);
            var cnt = BitConverter.ToInt32(m.Data, 20);
            var cnt2= (cnt + 3) / 4;
            var read= cnt2 * 4;
            var end = tbl + cnt2 * 4;
            m       = rance10.ReadMemory(new IntPtr(tbl), buf, read);

            if(!m.Success || m.ReadedSize != read)
                return;

            ObjectManagerAddress= mng1;
            ObjectCount         = cnt;
            TableCount          = cnt2;
            TableStart          = tbl;
            TableEnd            = (uint)(tbl + cnt2 * 4);
            TableAddresses      = Enumerable.Range(0, TableCount)
                                    .Select(i => BitConverter.ToUInt32(m.Data, i*4))
                                    .ToArray();
            var objs            = new List<uint>(ObjectCount);
            var rem             = cnt;
            var idx             = 0;

            System.Diagnostics.Debug.Print($"ObjectManagerAddress: {ObjectManagerAddress:X8}");
            System.Diagnostics.Debug.Print($"ObjectCount:          {ObjectCount:X8}");
            System.Diagnostics.Debug.Print($"TableCount:           {TableCount:X8}");
            System.Diagnostics.Debug.Print($"TableStart:           {TableStart:X8}");
            System.Diagnostics.Debug.Print($"TableEnd:             {TableEnd:X8}");

            Log.WriteLine("ObjectTable");

            foreach(var i in TableAddresses)
            {
                var tbladdr = TableStart + idx * 4;
                m           = rance10.ReadMemory(new IntPtr(i), buf, 16);

                if(m.Success && m.ReadedSize == 16)
                {
                    var a0  = BitConverter.ToUInt32(m.Data,  0);
                    var a1  = BitConverter.ToUInt32(m.Data,  4);
                    var a2  = BitConverter.ToUInt32(m.Data,  8);
                    var a3  = BitConverter.ToUInt32(m.Data, 12);

                    Log.WriteLine("\t#{0}\t{1:X8}->{2:X8}: {3:X8} {4:X8} {5:X8} {6:X8}",
                        idx++, tbladdr, i, a0, a1, a2, a3);

                    if(rem >= 1) objs.Add(a0);
                    if(rem >= 2) objs.Add(a1);
                    if(rem >= 3) objs.Add(a2);
                    if(rem >= 4) objs.Add(a3);
                } else
                    throw new Exception();

                rem -=4;
            }

            Log.Flush();

            ObjectAddresses     = objs.ToArray();

            System.Diagnostics.Debug.Assert(ObjectAddresses.Length == ObjectCount);
        }

        private void ReadObjects(Rance10 rance10)
        {
            var idx = 0;
            Objects = new Rance10Object[ObjectAddresses.Length];

            foreach(var i in ObjectAddresses)
            {
                var tbladdr     = TableStart + (idx / 4) * 4;

                Log.WriteLine($"\t#{idx/4}\t{tbladdr:X8}...{i:X8}");

                try
                {
                    var obj         = ReadObject(rance10, i, idx);
                    Objects[idx++]  = obj;

                    if(null != obj)
                        obj.Analyze(rance10);
                } catch(Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Log.Flush();
                    throw;
                }
            }
        }

        private Rance10Object ReadObject(Rance10 rance10, uint addr, int idx)
        {
            if(addr == 0)
                return null;

            var buf = new byte[64];
            var m   = rance10.ReadMemory(new IntPtr(addr), buf, 8);

            if(!m.Success)
            {
                System.Diagnostics.Debug.Print($"Failed to read memory: {addr:X8}");
                return null;
            }

            var type    = BitConverter.ToInt32(m.Data, 4);

            switch(type)
            {
            case 0: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject0       .Size); break;
            case 1: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject1_Value .Size); break;
            case 2: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject2_String.Size); break;
            case 3: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject3_Array .Size); break;
            case 4: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject4_Class .Size); break;
            case 5: m   = rance10.ReadMemory(new IntPtr(addr), buf, Rance10SubObject5.Size); break;
            default: throw new Exception($"Invalid type no: {type} @{addr:X8}");
            }

            if(!m.Success)
            {
                System.Diagnostics.Debug.Print($"Failed to read memory: {addr:X8}");
                return null;
            }

            var obj = Rance10Object.New(null, (uint)m.Address.ToInt64(), m.Data);
            obj.Seq = idx;

            return obj;
        }

        private void SortObjects(Rance10 rance10)
        {
            ObjectsByType = new[]
            {
                new Dictionary<int, Rance10Object>(),
                new Dictionary<int, Rance10Object>(),
                new Dictionary<int, Rance10Object>(),
                new Dictionary<int, Rance10Object>(),
                new Dictionary<int, Rance10Object>(),
                new Dictionary<int, Rance10Object>(),
            };

            foreach(var i in Objects.Where(i => i != null))
                ObjectsByType[i.Type].Add(i.Seq, i);

            ClassObject   = new Dictionary<string, List<Rance10Object>>();
            List<Rance10Object>   list;

            foreach(var i in ObjectsByType[4].Values)
            {
                var cname   = i.ClassName;

                if(!ClassObject.TryGetValue(cname, out list))
                    ClassObject.Add(cname, list= new List<Rance10Object>());

                list.Add(i);
            }
        }

        private void AnalyzeObjects(Rance10 rance10)
        {
            Log.WriteLine("");
            Log.WriteLine("Objects");

            var type    = 0;

            foreach(var i in ObjectsByType)
            {
                Log.WriteLine($"\t{type++}");

                foreach(var j in i.Values)
                    Log.WriteLine($"\t\t{j}");
            }

            Log.WriteLine("");
            Log.WriteLine("ClassObjects");

            foreach(var i in ClassObject)
            {
                var cname   = i.Key;

                Log.WriteLine($"\t{cname}");

                foreach(var j in i.Value)
                    AnalyzeObject(rance10, j);
            }
        }

        private void AnalyzeObject(Rance10 rance10, Rance10Object obj)
        {
            Log.WriteLine($"\t\t{obj}");

            if(null == obj.ObjectData)
                return;

            foreach(var i in obj.ObjectData)
            {
                var assoc   = GetObject((int)i);

                if(null == assoc)
                        Log.WriteLine($"\t\t\t{i:X8}->\tnull");
                else    Log.WriteLine($"\t\t\t{i:X8}->\t{assoc}");
            }
        }
    }
}
