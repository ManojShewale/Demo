using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static Demo.Controllers.HomeController;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml.Linq;

namespace Demo.Models
{
    public class GetData
    {
        public static TreeNode BuildTree(List<tblNode> items)
        {
            // Create a dictionary to store nodes by their IDs for fast lookup
            var nodeDictionary = new Dictionary<int, TreeNode>();

            // Create tree nodes for each item in the list
            foreach (var item in items)
            {
                var node = new TreeNode { Id = item.nodeId, Name = item.nodeName };
                nodeDictionary.Add(item.nodeId, node);
            }

            // Build the tree structure
            TreeNode root = null;
            foreach (var item in items)
            {
                TreeNode node = nodeDictionary[item.nodeId];
                if (item.parentNodeId == 0)
                {
                    // If ParentId is 0, it's the root node
                    root = node;
                }
                else
                {
                    // Find parent node and add current node as its child
                    TreeNode parent = nodeDictionary[item.parentNodeId];
                    parent.Children.Add(node);
                }
            }

            return root;
        }
    }

}