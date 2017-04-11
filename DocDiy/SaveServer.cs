/*
 * 由SharpDevelop创建。
 * 用户： Yue
 * 日期: 2017/4/10
 * 时间: 17:02
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace DocDiy
{
	/// <summary>
	/// Description of SaveServer.
	/// </summary>
	public class SaveServer
	{
		public const bool HTTP_MODE = true;
		public const bool FILE_MODE = false;
		private bool mode = HTTP_MODE;
		private XmlHelper helper;
		public SaveServer(bool mode)
		{
			this.mode = mode;
			helper = new XmlHelper();
		}
		public void save(string path){
			helper.saveDiyUri(path,mode);
		}
	}
}
