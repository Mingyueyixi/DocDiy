/*
 * 由SharpDevelop创建。
 * 用户： Yue
 * 日期: 2017/4/10
 * 时间: 11:12
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DocDiy
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private ModeModel mModel;
		private string path;
		private string jdktablePath;
		private XmlHelper helper;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			mModel = new ModeModel();
			helper = new XmlHelper();
			InitializeComponent();
			initConfigPath();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		private void initConfigPath(){
			path = getAndroidStudioConfigPath();
			adaperInfo(path);
			
		}
		private string getAndroidStudioConfigPath(){
			return getUserDir() + "\\.AndroidStudio2.3";
		}
		private string getUserDir(){
			return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		}
		void TextBox1DragDrop(object sender, DragEventArgs e)
		{
		string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
		adaperInfo(path);
		
		}
		private void adaperInfo(string path){
			textBox1.Text = path;
			this.path = path;
			jdktablePath = path+"\\config\\options\\jdk.table.xml";
			string[] paths = path.Split('\\');
			string fileName = paths[paths.Length-1];
			if(fileName.Equals(".AndroidStudio2.3")){
				
				string jdktableFile = path+"\\config\\options\\jdk.table.xml";
				if(!File.Exists(jdktablePath)){
					return;
				}
				label2.ForeColor = Color.Green;
				label2.Text = "适配成功";
				List<string> urls = helper.readUris(jdktableFile,"javadocPath");
				//				MessageBox.Show(urls.ToArray()[2],"测试");
				bool flag = mModel.isHttpMode(urls);
				if(flag){
					trueRadio2();
					textBox2.Text = mModel.httpList[0];
				}else{
					trueRadio1();
					textBox2.Text = mModel.fileList[0];
				}
			}
			else if(fileName.Contains(".AndroidStudio")){
				label2.ForeColor = Color.DarkOrange;
				label2.Text = "可能不适配";
			}
			else{
				label2.ForeColor = Color.Red;
				label2.Text = "不适配";
			}
			
		}
		void TextBox1DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)){
                  e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
			}else{
				e.Effect = DragDropEffects.None;
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string sourcefilepath = "";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
            	sourcefilepath = folderBrowserDialog.SelectedPath;     
            }
            else
            {
                sourcefilepath = "";
            } 
            textBox1.Text = sourcefilepath;
            adaperInfo(sourcefilepath);
		}
		void RadioButton1CheckedChanged(object sender, EventArgs e)
		{
			if(mModel.fileCount == 0){
				setSourcePath();
			}else{
				textBox2.Text = mModel.fileList[0];
			}
		}
		void RadioButton2CheckedChanged(object sender, EventArgs e)
		{
			if(mModel.httpCount != 0){
				textBox2.Text = mModel.httpList[0];
			}
		}
		private void setSourcePath(){
			if(String.IsNullOrEmpty(path)){
				return;
			}
			string jdktableFile = path+"\\config\\options\\jdk.table.xml";
			List<string> urls = helper.readUris(jdktableFile,"sourcePath");
			textBox2.Text = urls[0];
		}
		private void trueRadio1(){
			radioButton1.Checked = true;
//			radioButton2.Checked = false;
		}
		private void trueRadio2(){
//			radioButton1.Checked = false;
			radioButton2.Checked = true;
		}
		void Button2Click(object sender, EventArgs e)
		{
	System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            string sourcefilepath = "";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
            	sourcefilepath = folderBrowserDialog.SelectedPath;     
            }
            else
            {
                sourcefilepath = "";
            } 
            textBox2.Text = sourcefilepath;
		}
		void Button3Click(object sender, EventArgs e)
		{
			label3.Text = "修复中...";
			new SaveServer(radioButton2.Checked).save(jdktablePath);
			label3.Text = "完成，查看详情";
		}
		void Label3Click(object sender, EventArgs e)
		{
			if(label3.Text.Equals("完成，查看详情")){
				string jdktableDir = jdktablePath.Substring(0,jdktablePath.LastIndexOf("\\")+1);
				openFolderAndSelectFile(jdktablePath);
			}	
		}
		private void openFolderAndSelectFile(String absolutePath)
		{
    		System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
    		psi.Arguments = "/e,/select,"+absolutePath;
    		System.Diagnostics.Process.Start(psi);
		}
	}
}
