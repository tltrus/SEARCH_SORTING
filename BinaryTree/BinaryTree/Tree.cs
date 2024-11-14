

namespace BinaryTree
{
    internal class Tree
    {
        public Node root;

        public void Traverse()
        {
            root.Visit(root);
        }

        public Node Search(double value)
        {
            return root.Search(value);
        }

        public void AddValue(double value)
        {
            Node n = new Node(value);
            if (root == null)
            {
                root = n;
                root.x = MainWindow.width / 2;
                root.y = 16;

                MainWindow.list.Add(n.value);
            }
            else
            {
                root.AddNode(n);
            }
        }
    }

}
