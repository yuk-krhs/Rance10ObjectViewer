using System;
using System.Linq;
using System.Text;

namespace Rance10ObjectViewer
{
    public abstract class Rance10ObjectBase
    {
        public static Encoding          Encoding= Encoding.GetEncoding("Shift_JIS");

        public Rance10ObjectBase        Owner                   { get; protected set; }
        public uint                     Address                 { get; private set; }
        public byte[]                   Data                    { get; private set; }
        public uint                     VFTable                 { get { return GetUInt32( 0); } }
        public uint                     Value00                 { get { return GetUInt32( 0); } }
        public uint                     Value04                 { get { return GetUInt32( 4); } }
        public uint                     Value08                 { get { return GetUInt32( 8); } }
        public uint                     Value0C                 { get { return GetUInt32(12); } }
        public uint                     Value10                 { get { return GetUInt32(16); } }
        public uint                     Value14                 { get { return GetUInt32(20); } }
        public uint                     Value18                 { get { return GetUInt32(24); } }
        public uint                     Value1C                 { get { return GetUInt32(28); } }
        public uint                     Value20                 { get { return GetUInt32(32); } }
        public uint                     Value24                 { get { return GetUInt32(36); } }
        public uint                     Value28                 { get { return GetUInt32(40); } }
        public uint                     Value2C                 { get { return GetUInt32(44); } }
        public uint                     Value30                 { get { return GetUInt32(48); } }
        public uint                     Value34                 { get { return GetUInt32(52); } }
        public uint                     Value38                 { get { return GetUInt32(56); } }
        public uint                     Value3C                 { get { return GetUInt32(60); } }

        protected Rance10ObjectBase(uint addr, byte[] data, int size)
        {
            if(size != 0)
                Array.Resize(ref data, size);

            Address = addr;
            Data    = data;
        }

        public int  GetInt32(int off)   { return BitConverter.ToInt32(Data, off); }
        public uint GetUInt32(int off)  { return BitConverter.ToUInt32(Data, off); }

        public static string ReadString(IProcessAccessor a, uint addr)
        {
            var m       = a.ReadMemory(new IntPtr(addr), 20);

            if(!m.Success)
                return null;

            var len     = BitConverter.ToInt32(m.Data, 16);

            if(len >= 16)
                m       = a.ReadMemory(new IntPtr(BitConverter.ToUInt32(m.Data, 0)), len);

            if(!m.Success)
                return null;

            return Encoding.GetString(m.Data, 0, len);
        }

        public static string ReadStringZ(IProcessAccessor a, uint addr, int max)
        {
            var m       = a.ReadMemory(new IntPtr(addr), max);

            if(!m.Success)
                return null;

            var idx     = Array.IndexOf(m.Data, (byte)0);
            var len     = idx < 0 ? m.Data.Length : idx;

            return Encoding.GetString(m.Data, 0, len);
        }

        public override string ToString()
        {
            var cnt = Math.Min(16, Data.Length / 4);
            var sb  = new StringBuilder();

            sb.Append($"A:{Address:X8}\t");
          //sb.Append(string.Join("\t", Enumerable.Range(0, cnt).Select(i => $"{GetUInt32(i*4):X8}")));
            sb.Append(string.Join(" ", Enumerable.Range(0, cnt).Select(i => $"{GetUInt32(i*4):X8}")));

            return sb.ToString();
        }
    }

    public abstract class Rance10Object : Rance10ObjectBase
    {
        public string                   ClassName               { get; set; }
        public string                   ValueString             { get; set; }
        public uint[]                   ObjectData              { get; set; }
        public int                      Seq                     { get; set; }
        // Common
        public uint                     FuncTbl1                { get { return GetUInt32( 0); } }
        public uint                     Type                    { get { return GetUInt32( 4); } }
        public uint                     FuncTbl2                { get { return GetUInt32(12); } }
        public int                      UnknownNumber           { get { return GetInt32 (28); } }
        public uint                     FuncTbl3                { get { return GetUInt32(32); } }
        // Individual
        public uint                     DataPtr                 { get { return GetUInt32(16); } }
        public int                      DataSize1               { get { return GetInt32 (20); } }
        public int                      DataSize2               { get { return GetInt32 (24); } }
        public uint                     ObjectManager           { get { return GetUInt32(36); } }
        public uint                     ObjectId                { get { return GetUInt32(40); } }
        public uint                     ArrayBegin              { get { return GetUInt32(44); } }
        public uint                     ArrayEnd                { get { return GetUInt32(48); } }
        public uint                     ArrayCapacity           { get { return GetUInt32(52); } }
        public uint                     ClassNamePtr            { get { return GetUInt32(56); } }

        protected Rance10Object(Rance10ObjectBase owner, uint addr, byte[] data, int size)
            : base(addr, data, size)
        {
            Owner   = owner;
        }

        public abstract void Analyze(IProcessAccessor a);

        protected void ReadObjectData(IProcessAccessor a)
        {
            var m   = a.ReadMemory(new IntPtr(DataPtr), Math.Min(64, DataSize1));

            if(m.Success)
                ObjectData  = Enumerable.Range(0, m.Data.Length / 4)
                    .Select(i => BitConverter.ToUInt32(m.Data, i*4)).ToArray();
        }

        protected void ReadArrayData(IProcessAccessor a)
        {
            var cnt = DataSize1 / 4;
            var m   = a.ReadMemory(new IntPtr(DataPtr), Math.Min(64, cnt * 4));

            if(m.Success)
                ObjectData  = Enumerable.Range(0, m.Data.Length / 4)
                    .Select(i => BitConverter.ToUInt32(m.Data, i*4)).ToArray();
        }

        protected void ReadValueString(IProcessAccessor a)
        {
            var straddr = GetUInt32(4*4);
            ValueString = ReadStringZ(a, straddr, 256);
        }

        protected void ReadClassName(IProcessAccessor a)
        {
            var nameaddr= GetUInt32(14*4);
            ClassName   = ReadString(a, nameaddr);
        }

        public override string ToString()
        {
            var sb  = new StringBuilder();
            
            sb.Append($"#{Seq:X8}\t{base.ToString()}");

            if(null != ClassName)
                sb.Append($"\t{ClassName}");

            if(null != ValueString)
                sb.Append($"\t\"{ValueString}\"");

            if(null != ObjectData)
            {
              //sb.Append("\tData");
                sb.Append(" Data");

                foreach(var i in ObjectData)
                  //sb.Append($"\t{i:X8}");
                    sb.Append($" {i:X8}");
            }

            return sb.ToString();
        }

        public static Rance10Object New(Rance10ObjectBase owner, uint addr, byte[] data)
        {
            var type= BitConverter.ToInt32(data, 4);

            switch(type)
            {
            case 0: return new Rance10SubObject0       (owner, addr, data);
            case 1: return new Rance10SubObject1_Value (owner, addr, data);
            case 2: return new Rance10SubObject2_String(owner, addr, data);
            case 3: return new Rance10SubObject3_Array (owner, addr, data);
            case 4: return new Rance10SubObject4_Class (owner, addr, data);
            case 5: return new Rance10SubObject5       (owner, addr, data);
            default:throw new ArgumentException();
            }
        }
    }

    // type=0 vftbl=0x799024 size=44(0x2C)
    // 00000000 class_Object_0  struc ; (sizeof=0x2C, mappedto_121)
    // 00000000 base            class_ObjectBase ?      ; functbl_799024_obj0
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 data_ptr        dd ?
    // 00000014 zero_1          dd ?
    // 00000018 zero_2          dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 pObjMng         dd ?
    // 00000028 num_1           dd ?
    // 0000002C class_Object_0  ends
    public class Rance10SubObject0 : Rance10Object
    {
        public const int                Size                    = 0x2C;

        public Rance10SubObject0(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, 44)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
        }
    }

    // type=1 vftbl=0x798490 size=48(0x30)
    // 00000000 class_Object_1  struc ; (sizeof=0x30, mappedto_123)
    // 00000000 base            class_ObjectBase ?      ; functbl_798490_obj1
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 data_ptr        dd ?
    // 00000014 zero_1          dd ?
    // 00000018 zero_2          dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 pObjMng         dd ?                    ; offset
    // 00000028 _1_0            dd ?
    // 0000002C _1_1            dd ?
    // 00000030 class_Object_1  ends
    public class Rance10SubObject1_Value : Rance10Object
    {
        public const int                Size                    = 0x30;

        public Rance10SubObject1_Value(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, Size)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
        }
    }

    // type=2 vftbl=0x798E74 size=36(0x24)
    // 00000000 class_Object_2_String struc ; (sizeof=0x24, mappedto_114)
    // 00000000 base            class_ObjectBase ?      ; functbl_798E74_obj2_string
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 buffer          dd ?
    // 00000014 length          dd ?
    // 00000018 capacity        dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 class_Object_2_String ends
    public class Rance10SubObject2_String : Rance10Object
    {
        public const int                Size                    = 0x24;

        public uint                     Buffer                  { get { return GetUInt32(0x10); } }
        public int                      Length                  { get { return GetInt32 (0x14); } }
        public int                      Capacity                { get { return GetInt32 (0x18); } }

        public Rance10SubObject2_String(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, Size)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
            ReadValueString(a);
        }
    }

    // type=3 vftbl=0x798EE8 size=64(0x40)
    // 00000000 class_Object_3_Array struc ; (sizeof=0x40, mappedto_118)
    // 00000000 base            class_ObjectBase ?      ; functbl_798EE8_obj3_array
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 data_ptr        dd ?
    // 00000014 zero_1          dd ?
    // 00000018 zero_2          dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 pObjMng         dd ?                    ; offset
    // 00000028 id              dd ?
    // 0000002C _1_0            dd ?
    // 00000030 _1_1            dd ?
    // 00000034 zero_3          dd ?
    // 00000038 zero_4          db ?
    // 00000039 padding         db 3 dup(?)
    // 0000003C zero_5          dd ?
    // 00000040 class_Object_3_Array ends
    public class Rance10SubObject3_Array : Rance10Object
    {
        public const int                Size                    = 0x40;

        public Rance10SubObject3_Array(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, Size)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
            ReadArrayData(a);
        }
    }

    // type=4 vftbl=0x799138 size=60(0x3C)
    // 00000000 class_Object_4_Class struc ; (sizeof=0x3C, mappedto_119)
    // 00000000 base            class_ObjectBase ?      ; functbl_799138_obj4_class
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 data_ptr        dd ?
    // 00000014 zero_1          dd ?
    // 00000018 zero_2          dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 pObjMng         dd ?                    ; offset
    // 00000028 id              dd ?
    // 0000002C zero_3          dd ?
    // 00000030 zero_4          dd ?
    // 00000034 zero_5          dd ?
    // 00000038 zero_6          dd ?
    // 0000003C class_Object_4_Class ends
    public class Rance10SubObject4_Class : Rance10Object
    {
        public const int                Size                    = 0x3C;

        public Rance10SubObject4_Class(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, Size)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
            ReadClassName(a);
            ReadObjectData(a);
        }
    }

    // Image?
    //
    // type=5 vftbl=0x79843C size=52(0x34)
    // 00000000 class_Object_5 struc ; (sizeof=0x34, mappedto_120)
    // 00000000 base            class_ObjectBase ?      ; functbl_79843C_obj5
    // 00000008 zero            dd ?
    // 0000000C functbl_1       dd ?                    ; offset
    // 00000010 data_ptr        dd ?
    // 00000014 zero_1          dd ?
    // 00000018 zero_2          dd ?
    // 0000001C num_0           dd ?
    // 00000020 functbl_2       dd ?                    ; offset
    // 00000024 pObjMng         dd ?                    ; offset
    // 00000028 id              dd ?
    // 0000002C zero_3          dd ?
    // 00000030 zero_4          dd ?
    // 00000034 class_Object_5_Struct ends
    public class Rance10SubObject5 : Rance10Object
    {
        public const int                Size                    = 0x34;

        public Rance10SubObject5(Rance10ObjectBase owner, uint addr, byte[] data)
            : base(owner, addr, data, Size)
        {
        }

        public override void Analyze(IProcessAccessor a)
        {
            ReadArrayData(a);
        }
    }
}
