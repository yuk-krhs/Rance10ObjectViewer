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
    public partial class Rance10AnalyzeView : UserControl
    {
        private Rance10ObjectAnalyzer   analyzer;

        public Rance10ObjectAnalyzer    Analyzer                { get { return analyzer; } set { SetAnalyzer(value); } }

        public Rance10AnalyzeView()
        {
            InitializeComponent();
        }

        private void tvObject_DoubleClick(object sender, EventArgs e)
        {
            var p   = tvObject.PointToClient(Control.MousePosition);
            var hti = tvObject.HitTest(p);

            if(hti.Node == null)
                return;

            var node= hti.Node;

            if(node.Tag is uint)
            {
                var seq = (uint)hti.Node.Tag;
                var obj = analyzer.GetObject((int)seq);

                if(obj == null)
                {
                    node.ForeColor  = Color.Red;
                    return;
                }

                node.Tag= obj;
                node.Text   = $"[{node.Index}] {NodeText(obj)}";

                if(null != obj.ObjectData)
                    foreach(var i in obj.ObjectData)
                        AddNode(node.Nodes, i);

                node.Expand();

                tvObject.SelectedNode   = node;
            }
        }

        private void tvObject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null)
            {
                tbInfo.Text = "";
            } else
            if(e.Node.Tag is Rance10Object)
            {
                tbInfo.Text = e.Node.Tag.ToString();
            } else
            if(e.Node.Tag is uint)
            {
                tbInfo.Text = "";
            } else
                tbInfo.Text = "";
        }

        private void tsbRefreshObjectTree_Click(object sender, EventArgs e)
        {
            if(!tsbRefreshObjectTree.Enabled)
                return;

            tsbRefreshObjectTree.Enabled    = false;

            try
            {
                SuspendLayout();
                tvObject.Visible        = false;

                try
                {
                    UpdateObjectTree();
                    Application.DoEvents();
                } finally
                {
                    tvObject.Visible    = true;
                    SuspendLayout();
                }
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                MessageBox.Show(ex.ToString());
            } finally
            {
                tsbRefreshObjectTree.Enabled= true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == (Keys.C | Keys.Control))
            {
                // 選択ノード以下のオブジェクトツリーをテキストとしてクリップボードにコピー
                var node= tvObject.SelectedNode;
                var sb  = new StringBuilder();

                AppendNodeText(sb, node, 0);
                Clipboard.SetText(sb.ToString());

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetAnalyzer(Rance10ObjectAnalyzer value)
        {
            if(value == analyzer)
                return;

            analyzer    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            tvObject.Nodes.Clear();

            if(null == analyzer)
                return;

            foreach(var i in analyzer.ClassObject.Keys.OrderBy(i => i))
            {
                List<Rance10Object>   objs;

                if(!analyzer.ClassObject.TryGetValue(i, out objs))
                    continue;

                var node= tvObject.Nodes.Add(i);

                foreach(var j in objs)
                    AddNode(node.Nodes, j).Expand();

                //node.Expand();
            }
        }

        private TreeNode AddNode(TreeNodeCollection nodes, uint value)
        {
            var node= nodes.Add($"[{nodes.Count}] #{value:X8}");
            node.Tag= value;

            return node;
        }

        private TreeNode AddNode(TreeNodeCollection nodes, Rance10Object obj)
        {
            var node= nodes.Add(NodeText(obj));
            node.Tag= obj;

            if(null != obj.ObjectData)
                foreach(var i in obj.ObjectData)
                    AddNode(node.Nodes, i);

            return node;
        }

        private string NodeText(Rance10Object value)
        {
            switch(value.Type)
            {
            case 0: return $"#{value.Seq:X8} @{value.Address:X8} 0";
            case 1: return $"#{value.Seq:X8} @{value.Address:X8} 1";
            case 2: return $"#{value.Seq:X8} @{value.Address:X8} 2 \"{value.ValueString}\"";
            case 3: return $"#{value.Seq:X8} @{value.Address:X8} 3";
            case 4: return $"#{value.Seq:X8} @{value.Address:X8} 4 {value.ClassName}";
            case 5: return $"#{value.Seq:X8} @{value.Address:X8} 5";
            default:    throw new Exception();
            }
        }

        private void UpdateObjectTree()
        {
            using(var rance10= Rance10.Create())
            {
                var ana = rance10.AnalyzeObjTable();
                Analyzer= ana;
            }
        }

        private void AppendNodeText(StringBuilder sb, TreeNode node, int level)
        {
            if(level > 0)
                sb.Append(new string('\t', level));

            sb.AppendLine(node.Text);

            foreach(TreeNode i in node.Nodes)
                AppendNodeText(sb, i, level+1);
        }
    }
}
