using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using SubDown.Codes;
using SubDown.Downloaders;
using System.Threading;

namespace SubDown
{
    public partial class SubDownForm : Form
    {
        private bool buscando = false;
        private Thread buscaThread;
        private Thread loginThread;

        public SubDownForm()
        {
            InitializeComponent();
        }

        private void SubDownForm_Load(object sender, EventArgs e)
        {
            string path = @"cache";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            textBoxUsuario.Text = Properties.Settings.Default.Usuario;
            textBoxSenha.Text = Properties.Settings.Default.Senha;
            if (Properties.Settings.Default.Folders != null)
            {
                foreach (string folder in Properties.Settings.Default.Folders)
                {
                    listBoxDiretorios.Items.Add(folder);
                }
            }

            comboBoxTipoPesquisa.SelectedIndex = Properties.Settings.Default.TipoPesquisa;

            LegendasTV downloader = new LegendasTV();
            loginThread = new Thread(downloader.ThreadlogIn);
            loginThread.Name = "Thread Login";
            loginThread.Start();

            bool pesquisaFilmes = comboBoxTipoPesquisa.SelectedIndex == 1;

            atualizarArquivos(pesquisaFilmes);
        }

        private void buttonBaixarLegenda_Click(object sender, EventArgs e)
        {
            if (!buscando)
            {
                Properties.Settings.Default.Usuario = textBoxUsuario.Text;
                Properties.Settings.Default.Senha = textBoxSenha.Text;

                bool pesquisaFilmes = comboBoxTipoPesquisa.SelectedIndex == 1;

                atualizarArquivos(pesquisaFilmes);
                ThreadStart buscaThread = delegate { procurarLegendas(pesquisaFilmes); };
                new Thread(buscaThread).Start();

                buscando = true;
            }
        }

        delegate void addSubFoundCallBack(int nodeIndex, string name, string tag);
        public void addSubFound(int nodeIndex, string name, string tag)
        {
            if (treeViewLegendas.InvokeRequired)
            {
                addSubFoundCallBack d = new addSubFoundCallBack(addSubFound);
                this.Invoke(d, new object[] { nodeIndex, name, tag });
            }
            else
            {
                TreeNode node = treeViewLegendas.Nodes[nodeIndex].Nodes.Add(name);
                node.Tag = tag;
            }
        }

        delegate void removeSubFoundCallBack(int nodeIndex);
        public void removeSubFound(int nodeIndex)
        {
            if (treeViewLegendas.InvokeRequired)
            {
                removeSubFoundCallBack d = new removeSubFoundCallBack(removeSubFound);
                this.Invoke(d, new object[] { nodeIndex });
            }
            else
            {
                treeViewLegendas.Nodes[nodeIndex].Text = "Baixado - " + treeViewLegendas.Nodes[nodeIndex].Text;
            }
        }

        delegate void addLogCallBack(string msg);
        public void addLog(string msg)
        {
            if (textBoxLog.InvokeRequired)
            {
                addLogCallBack d = new addLogCallBack(addLog);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                textBoxLog.Text += DateTime.Now.ToString("[HH:mm:ss] ") + msg + Environment.NewLine;
                textBoxLog.SelectionStart = textBoxLog.TextLength;
                textBoxLog.ScrollToCaret();

            }
        }

        private void procurarLegendas(bool pesquisaFilmes)
        {
            foreach (TreeNode node in treeViewLegendas.Nodes)
            {
                if (node == null || node.Text == "")
                    continue;
                
                procurarLegenda(node.Text, node.Index, node.Tag.ToString(), pesquisaFilmes);
            }

            buscando = false;
        }

        private void procurarLegenda(string name, int index, string toFile, bool filme)
        {
            LegendasTV legendasTV = new LegendasTV();
            Release release = new Release(name,!filme);
            //MessageBox.Show(release.print());

            string searchString = (release.title + " " + release.getEpisode()).Trim();
            if (searchString == "")
                return;

            addLog("Procurando: " + searchString);
            List<Result> ret = legendasTV.SearchSubtitle(searchString);

            if (ret.Count == 0)
            {
                ret = legendasTV.SearchSubtitle(release.fileName);
                ret.Concat(legendasTV.SearchSubtitle((release.title + " " + release.getSeason()).Trim()));
            }

            foreach (Result result in ret)
            {
                Release other = new Release(result.name);
                if (release.Compare(other))
                {
                    addLog("Baixando: " + result.name);
                    string ext = baixarLegenda(result.id);
                    //addLog("Baixado.");

                    string[] fileList = Compressed.List(ext, "cache\\" + result.id + ext);
                    foreach (string rarFile in fileList)
                    {
                        if (rarFile == "")
                            continue;
                        
                        Release o = new Release(Path.GetFileNameWithoutExtension(rarFile));
                        if (release.Compare(o))
                        {
                            addLog("Extraindo: " + rarFile);
                            extrairLegenda(ext, result.id, rarFile, Path.GetDirectoryName(toFile) + "\\" + Path.GetFileNameWithoutExtension(toFile) + ".srt");
                            //addLog("Extraido.");

                            removeSubFound(index);
                            return;
                        }
                    }
                }
            }

            foreach (Result result in ret)
            {
                addSubFound(index, result.name, result.id);
            }
        }

        private string baixarLegenda(string subId)
        {
            LegendasTV downloader = new LegendasTV();
            return downloader.DownloadSubtitle(subId);
        }

        private void extrairLegenda(string ext, string subId, string file, string destFile)
        {
            //Compressed.Extract(ext, "cache\\" + subId + ext, file, "");

            Compressed.Extract("cache\\" + subId + ext, file, destFile);

            /*Thread.Sleep(100);

            if (File.Exists(Path.GetFileName(file)))
            {
                //addLog("Movendo " + Path.GetFileName(file) + " " + destFile);
                File.Move(Path.GetFileName(file), destFile);
                addLog("Pronto.");
                //addLog("Movido.");
            }*/
        }

        private void SubDownForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Usuario = textBoxUsuario.Text;
            Properties.Settings.Default.Senha = textBoxSenha.Text;
            Properties.Settings.Default.Folders = new System.Collections.Specialized.StringCollection();

            foreach (string folder in listBoxDiretorios.Items)
            {
                Properties.Settings.Default.Folders.Add(folder);
            }

            Properties.Settings.Default.TipoPesquisa = comboBoxTipoPesquisa.SelectedIndex;

            Properties.Settings.Default.Save();

            if(loginThread != null)
                loginThread.Abort();
            
            if(buscaThread != null)
                buscaThread.Abort();
        }

        private void adicionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool pesquisaFilmes = comboBoxTipoPesquisa.SelectedIndex == 1;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBoxDiretorios.Items.Add(dialog.SelectedPath);
            }

            atualizarArquivos(pesquisaFilmes);
        }

        private void deletarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool pesquisaFilmes = comboBoxTipoPesquisa.SelectedIndex == 1;

            if (listBoxDiretorios.SelectedIndex >= 0)
            {
                listBoxDiretorios.Items.RemoveAt(listBoxDiretorios.SelectedIndex);
            }

            atualizarArquivos(pesquisaFilmes);
        }

        public void atualizarArquivos(bool pesquisaFilmes)
        {
            treeViewLegendas.Nodes.Clear();

            foreach (string folder in listBoxDiretorios.Items)
            {
                List<string> files = Files.getFilesFromFolder(folder);

                foreach (string file in files)
                {
                    TreeNode node = new TreeNode();
                    if (pesquisaFilmes)
                        node = treeViewLegendas.Nodes.Add(Path.GetFileName(Path.GetDirectoryName(file)));
                    else
                        node = treeViewLegendas.Nodes.Add(Path.GetFileNameWithoutExtension(file));
                    node.Tag = file;
                }
            }
        }

        delegate void addFileCallBack(TreeNode node, string fileName, string ext);
        public void addFile(TreeNode node, string fileName, string ext)
        {
            if (treeViewLegendas.InvokeRequired)
            {
                addFileCallBack d = new addFileCallBack(addFile);
                this.Invoke(d, new object[] { node, fileName, ext });
            }
            else
            {
                TreeNode sub = node.Nodes.Add(fileName);
                sub.Tag = ext;
            }
        }

        private void clickBaixarLegenda(object _node)
        {
            TreeNode node = (TreeNode)_node;
            string ext = baixarLegenda(node.Tag.ToString());
            string[] list = Compressed.List(ext, "cache\\" + node.Tag.ToString() + ext);

            foreach (string f in list)
            {
                if (f != "" && Path.GetExtension(f) == ".srt")
                {
                    addFile(node, f, ext);
                }
            }
        }

        private void treeViewLegendas_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = treeViewLegendas.SelectedNode;
            if (node == null)
            {
                return;
            }

            if (node.Parent != null && node.Tag.ToString() != ".zip" && node.Tag.ToString() != ".rar" && node.Nodes.Count == 0)
            {
                Thread baixarLegendaThread = new Thread(new ParameterizedThreadStart(clickBaixarLegenda));
                baixarLegendaThread.Name = "Baixar Legenda Click Thread";
                baixarLegendaThread.Start(node);
            }
            else if (node.Parent != null && (node.Tag.ToString() == ".zip" || node.Tag.ToString() == ".rar"))
            {
                string subId = node.Parent.Tag.ToString();
                string ext = node.Tag.ToString();
                string file = node.Text;
                string originalFile = node.Parent.Parent.Tag.ToString();
                string destFile = Path.GetDirectoryName(originalFile) + "\\" + Path.GetFileNameWithoutExtension(originalFile) + ".srt";

                addLog(subId + " " + ext + " " + file + " " + destFile);
                
                extrairLegenda(ext, subId, file, destFile);
                removeSubFound(node.Parent.Parent.Index);
            }
        }

        private void adicionarPastaButton_Click(object sender, EventArgs e)
        {
            bool pesquisaFilmes = comboBoxTipoPesquisa.SelectedIndex == 1;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBoxDiretorios.Items.Add(dialog.SelectedPath);
            }

            atualizarArquivos(pesquisaFilmes);
        }
    }
}
