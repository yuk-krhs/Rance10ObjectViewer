using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rance10ObjectViewer
{
    public partial class CheatControl : UserControl
    {
        public CheatControl()
        {
            InitializeComponent();
        }

        private void PartyMembers()
        {
            try
            {
                using(var rance10= Rance10.Create())
                {
                    var ana = new Rance10ObjectAnalyzer();

                    ana.Analyze(rance10);

                    var party   = new Party(ana, ana.ClassObject["SceneParty"].First());
                    var sb      = new StringBuilder();

                    sb.AppendLine("AT------ HP------ Rank---- Name----------------");

                    foreach(var i in party.Leaders)
                    {
                        if(i.PlayerCard == null)
                            continue;

                        sb.AppendFormat("{0} {1} {2} {3}",
                            i.AT.ToString("N0")  .PadLeft(8),
                            i.HP.ToString("N0")  .PadLeft(8),
                            i.Rank.ToString("N0").PadLeft(8),
                            i.CardName).AppendLine();
                    }

                    tbInfo.Text = sb.ToString();
                }
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void bMaxExpFriend_Click(object sender, EventArgs e)
        {
            try
            {
                using(var rance10= Rance10.Create())
                {
                    var ana = new Rance10ObjectAnalyzer();

                    ana.Analyze(rance10);

                    List<Rance10Object>     objs;

                    if(!ana.ClassObject.TryGetValue("PlayerCommonParam", out objs))
                        return;

                    var param   = new CommonParam(ana, objs.First());

                    if(cbTicket    .Checked)    param.WriteTicket    (rance10, 3);          // 食券
                    if(cbFriendship.Checked)    param.WriteFriendship(rance10, 3);          // 友情
                    if(cbMedal     .Checked)    param.WriteMedal     (rance10, 2);          // 勲章
                    if(cbIngot     .Checked)    param.WriteIngot     (rance10, 3);          // 金塊
                    if(cbTotalExp  .Checked)    param.WriteTotalExp  (rance10, 99999999);   // 獲得経験値
                    if(cbUltimate  .Checked)    UltimateParty(rance10, ana);
                }
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void UltimateParty(Rance10 Rance10, Rance10ObjectAnalyzer ana)
        {
            // 編成画面のみで機能する
            // リーダーのパラメータを書き換える
            //
            // 以下の手順で実際のゲーム中に反映される
            //   1.編成画面
            //   2."Write params" を押す
            //   3.クエスト画面に戻る
            //   4.編成画面
            //   5.クエスト画面に戻る
            var party   = new Party(ana, ana.ClassObject["SceneParty"].First());
            var data    = new byte[8];

            Array.Copy(BitConverter.GetBytes(999999), 0, data, 0, 4);   // AT
            Array.Copy(BitConverter.GetBytes(999999), 0, data, 4, 4);   // HP

            foreach(var i in party.Leaders)
            {
                if(i.PlayerCard == null)
                    continue;

                System.Diagnostics.Debug.Print("Write: {0:X8}", i.ParamAddress);

                Rance10.WriteMemory(new IntPtr(i.ParamAddress), data);
            }
        }

        private void bTurn_Click(object sender, EventArgs e)
        {
            try
            {
                if(!cbTurn.Checked)
                    return;

                using(var rance10= Rance10.Create())
                {
                    var ana = new Rance10ObjectAnalyzer();

                    ana.Analyze(rance10);

                    List<Rance10Object>     objs;

                    if(!ana.ClassObject.TryGetValue("GameContext", out objs))
                        return;

                    var gamectx = objs[0];
                    var trun    = (int)nudTurn.Value;

                    rance10.WriteMemory(new IntPtr(gamectx.DataPtr+8), BitConverter.GetBytes(trun));
                }
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class ObjectBase
    {
        public Rance10ObjectAnalyzer    Analyzer                { get; private set; }
        public Rance10Object            BaseObject              { get; private set; }

        public ObjectBase(Rance10ObjectAnalyzer ana, Rance10Object obj)
        {
            Analyzer    = ana;
            BaseObject  = obj;
        }

        public T Get<T>(ref T obj, uint seq) where T : Rance10Object
        {
            if(obj == null)
                obj = (T)Analyzer.GetObject((int)seq);

            return obj;
        }
    }

    public class CommonParam : ObjectBase
    {
        public Rance10Object            PlayerCommonParam       { get { return BaseObject; } }
        public uint                     FriendshipPoint         { get { return PlayerCommonParam.ObjectData[2]; } }
        public uint                     TotalExp                { get { return PlayerCommonParam.ObjectData[7]; } }
        public uint                     DataAddress             { get { return PlayerCommonParam.DataPtr; } }

        public CommonParam(Rance10ObjectAnalyzer ana, Rance10Object obj)
            : base(ana, obj)
        {
        }

        public void WriteParam(IProcessAccessor pa, int index, uint value)
        {
            pa.WriteMemory(new IntPtr(DataAddress + index * 4), BitConverter.GetBytes(value));
        }

        public void WriteTicket(IProcessAccessor pa, uint value)
        {
            WriteParam(pa, 0, value);
        }

        public void WriteIngot(IProcessAccessor pa, uint value)
        {
            WriteParam(pa, 0, value);
        }

        public void WriteMedal(IProcessAccessor pa, uint value)
        {
            WriteParam(pa, 1, value);
        }

        public void WriteFriendship(IProcessAccessor pa, uint value)
        {
            WriteParam(pa, 2, value);
        }

        public void WriteTotalExp(IProcessAccessor pa, uint value)
        {
            WriteParam(pa, 7, value);
        }
    }

    public class Party : ObjectBase
    {
        private List<Leader>            leaders;

        public Party(Rance10ObjectAnalyzer ana, Rance10Object obj)
            : base(ana, obj)
        {
        }

        public Rance10Object            SceneParty              { get { return BaseObject; } }
        public Rance10Object            FormationViewGroup      { get { return Get(ref _FormationViewGroup,      SceneParty             .ObjectData[4]); } } private Rance10Object _FormationViewGroup;
        public Rance10Object            FormationViewGroupItems { get { return Get(ref _FormationViewGroupItems, FormationViewGroup     .ObjectData[7]); } } private Rance10Object _FormationViewGroupItems;
        public Rance10Object            FormationView0          { get { return Get(ref _FormationView0,          FormationViewGroupItems.ObjectData[0]); } } private Rance10Object _FormationView0;
        public Rance10Object            FormationView1          { get { return Get(ref _FormationView1,          FormationViewGroupItems.ObjectData[1]); } } private Rance10Object _FormationView1;
        public Rance10Object            FormationView2          { get { return Get(ref _FormationView2,          FormationViewGroupItems.ObjectData[2]); } } private Rance10Object _FormationView2;
        public Rance10Object            FormationView3          { get { return Get(ref _FormationView3,          FormationViewGroupItems.ObjectData[3]); } } private Rance10Object _FormationView3;
        public Rance10Object            FormationView4          { get { return Get(ref _FormationView4,          FormationViewGroupItems.ObjectData[4]); } } private Rance10Object _FormationView4;
        public Rance10Object            FormationView5          { get { return Get(ref _FormationView5,          FormationViewGroupItems.ObjectData[5]); } } private Rance10Object _FormationView5;
        public Rance10Object            FormationView6          { get { return Get(ref _FormationView6,          FormationViewGroupItems.ObjectData[6]); } } private Rance10Object _FormationView6;

        public List<Leader>             Leaders
        {
            get
            {
                if(null != leaders)
                    return leaders;

                leaders = new List<Leader>();

                try { leaders.Add(new Leader(Analyzer, FormationView0)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView1)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView2)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView3)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView4)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView5)); } catch { }
                try { leaders.Add(new Leader(Analyzer, FormationView6)); } catch { }

                return leaders;
            }
        }
    }

    public class Leader : ObjectBase
    {
        public Rance10Object            FormationView           { get { return BaseObject; } }
        public Rance10Object            PlayerCard              { get { return Get(ref _PlayerCard,              FormationView          .ObjectData[2]); } } private Rance10Object _PlayerCard;
        public uint                     AT                      { get { return PlayerCard.ObjectData[0]; } }
        public uint                     HP                      { get { return PlayerCard.ObjectData[1]; } }
        public uint                     Rank                    { get { return PlayerCard.ObjectData[2]; } }
        public Rance10Object            CardNameObject          { get { return Get(ref _CardNameObject,          PlayerCard             .ObjectData[9]); } } private Rance10Object _CardNameObject;
        public string                   CardName                { get { return CardNameObject.ValueString; } }
        public uint                     ParamAddress            { get { return PlayerCard.DataPtr; } }

        public Leader(Rance10ObjectAnalyzer ana, Rance10Object obj)
            : base(ana, obj)
        {
        }
    }
}
