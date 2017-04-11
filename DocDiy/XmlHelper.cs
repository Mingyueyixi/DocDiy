/*
 * 由SharpDevelop创建。
 * 用户： Yue
 * 日期: 2017/4/10
 * 时间: 13:37
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;

namespace DocDiy
{
	/// <summary>
	/// Description of XmlHelper.
	/// </summary>
	public class XmlHelper
	{
		private XmlDocument doc;
		public XmlHelper()
		{
			doc = new XmlDocument();
		}
		public List<string> readUris(string path,string node){
			List<string> uriList = new List<string>();
			doc.Load(path);
			XmlNodeList nodes=doc.GetElementsByTagName(node);
			for(int i = 0;i<nodes.Count;i++){
				
				XmlNode urlNode = getRootUrlNode(nodes[i]);
				if(urlNode == null){
					continue;
				}
				string urlValue = getUrlValue(urlNode);
				uriList.Add(urlValue);
				}
			return uriList;
		}
		
		
		public void saveDiyUri(string path,bool flag){
			doc.Load(path);
			doc.Save(path+".bak");
			XmlNodeList jdkNodes = doc.GetElementsByTagName("jdk");
			string httpLink = null;
			for(int i = 0;i<jdkNodes.Count;i++){
				XmlNode jdkNode = jdkNodes[i];
				if(jdkNode==null){
					continue;
				}
				XmlNode childRoot = jdkNode.SelectSingleNode("roots");
				if(childRoot == null){
					continue;
				}
					XmlNode javadocPathNode = childRoot.SelectSingleNode("javadocPath");
					XmlNode sourcePathNode = childRoot.SelectSingleNode("sourcePath");
					if(javadocPathNode == null || sourcePathNode == null){
						continue;
					}
					XmlNode javadocUrlNode = getRootUrlNode(javadocPathNode);
					XmlNode sourceUrlNode = getRootUrlNode(sourcePathNode);
					
					if(javadocUrlNode == null){
						continue;
					}
					string javadocUrlValue = getUrlValue(javadocUrlNode);
					if(String.IsNullOrEmpty(httpLink) && sourceUrlNode == null ){
						httpLink = javadocUrlValue;
						continue;
					}
					if(sourceUrlNode == null){
						continue;
					}
					string sourceUrlValue = getUrlValue(sourceUrlNode);
					
//					if(sourceUrlValue == null || javadocUrlValue.Equals(sourceUrlValue)){
//						continue;
//					}
					
					if(flag == SaveServer.FILE_MODE){
						javadocUrlNode.Attributes["url"].Value = sourceUrlValue;
					}else{
						javadocUrlNode.Attributes["url"].Value = httpLink;
					}
				}
				doc.Save(path);
				
			}
		private string getUrlValue(XmlNode urlNode){
			XmlAttribute attr = urlNode.Attributes["url"];
			if(attr == null){
				return null;
			}
			return attr.Value;
		}
		private XmlNode getRootUrlNode(XmlNode pathNode){
			if(pathNode == null){
				return null;
			}
			XmlNode childRoot = pathNode.SelectSingleNode("root");
			if(childRoot == null){
				return null;
			}
			return childRoot.SelectSingleNode("root");
		}
		}
}