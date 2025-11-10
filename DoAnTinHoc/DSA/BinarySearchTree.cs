using DoAnTinHoc.Models;
using System.Collections.Generic;

namespace DoAnTinHoc.Data
{
    public class BSTNode
    {
        public Product Value { get; set; }
        public BSTNode? Left { get; set; }
        public BSTNode? Right { get; set; }
        public BSTNode(Product p) => Value = p;
    }

    public class BinarySearchTree
    {
        public BSTNode? Root { get; private set; }


        public void Insert(Product p)
        {
            Root = InsertRec(Root, p);
        }

        public void InsertOrUpdate(Product p) => Insert(p);

        private BSTNode InsertRec(BSTNode? node, Product p)
        {
            if (node == null) return new BSTNode(p);
            if (p.Id < node.Value.Id) node.Left = InsertRec(node.Left, p);
            else if (p.Id > node.Value.Id) node.Right = InsertRec(node.Right, p);
            else node.Value = p; // update khi trùng Id
            return node;
        }

        // Tìm Product theo id
        public Product? Search(int id)
        {
            var n = Root;
            while (n != null)
            {
                if (id == n.Value.Id) return n.Value;
                n = id < n.Value.Id ? n.Left : n.Right;
            }
            return null;
        }

        // Trả node (dùng khi cần truy xuất/thay đổi node trực tiếp)
        public BSTNode? FindNode(int id)
        {
            var n = Root;
            while (n != null)
            {
                if (id == n.Value.Id) return n;
                n = id < n.Value.Id ? n.Left : n.Right;
            }
            return null;
        }

        // Xóa theo id
        public void Delete(int id)
        {
            Root = DeleteRec(Root, id);
        }

        private BSTNode? DeleteRec(BSTNode? node, int id)
        {
            if (node == null) return null;

            if (id < node.Value.Id)
            {
                node.Left = DeleteRec(node.Left, id);
            }
            else if (id > node.Value.Id)
            {
                node.Right = DeleteRec(node.Right, id);
            }
            else
            {
                // node to delete found
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                // node with two children: replace with inorder successor (min in right)
                var minNode = FindMinNode(node.Right);
                if (minNode != null)
                {
                    node.Value = minNode.Value;
                    node.Right = DeleteRec(node.Right, minNode.Value.Id);
                }
            }
            return node;
        }

        private BSTNode? FindMinNode(BSTNode? node)
        {
            if (node == null) return null;
            while (node.Left != null) node = node.Left;
            return node;
        }

        // In-order traversal trả danh sách đã sort theo Id
        public List<Product> InOrder()
        {
            var list = new List<Product>();
            void Traverse(BSTNode? n)
            {
                if (n == null) return;
                Traverse(n.Left);
                list.Add(n.Value);
                Traverse(n.Right);
            }
            Traverse(Root);
            return list;
        }
    }
}
