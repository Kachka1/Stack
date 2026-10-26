using System;
using System.Collections.Generic;
using System.Text;

namespace TransportSystemsStack
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;
        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class MinStack
    {
        private Stack<int> stack;
        private Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int val)
        {
            stack.Push(val);
            if (minStack.Count == 0 || val <= minStack.Peek())
            {
                minStack.Push(val);
            }
        }

        public void Pop()
        {
            if (stack.Count > 0)
            {
                if (stack.Peek() == minStack.Peek())
                {
                    minStack.Pop();
                }
                stack.Pop();
            }
        }

        public int Top()
        {
            return stack.Peek();
        }

        public int GetMin()
        {
            return minStack.Peek();
        }
    }

    public class MyQueue
    {
        private Stack<int> inputStack;
        private Stack<int> outputStack;

        public MyQueue()
        {
            inputStack = new Stack<int>();
            outputStack = new Stack<int>();
        }

        public void Push(int x)
        {
            if (x < 1 || x > 9)
                throw new ArgumentException("Елемент має бути в межах [1, 9].");
            inputStack.Push(x);
        }

        public int Pop()
        {
            EnsureOutputStackHasElements();
            return outputStack.Pop();
        }

        public int Peek()
        {
            EnsureOutputStackHasElements();
            return outputStack.Peek();
        }

        public bool Empty()
        {
            return inputStack.Count == 0 && outputStack.Count == 0;
        }

        private void EnsureOutputStackHasElements()
        {
            if (outputStack.Count == 0)
            {
                while (inputStack.Count > 0)
                {
                    outputStack.Push(inputStack.Pop());
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== 1 ===");
            {
                string s1 = "()";
                Console.WriteLine($"1: Input: s = \"{s1}\" -> Output: {IsValid(s1).ToString().ToLower()}");

                string s2 = "()[]{}";
                Console.WriteLine($"2: Input: s = \"{s2}\" -> Output: {IsValid(s2).ToString().ToLower()}");

                string s3 = "(]";
                Console.WriteLine($"3: Input: s = \"{s3}\" -> Output: {IsValid(s3).ToString().ToLower()}\n");
            }

            Console.WriteLine("=== 2. ===");
            {
                TreeNode root1 = new TreeNode(1);
                root1.right = new TreeNode(2);
                root1.right.left = new TreeNode(3);
                Console.WriteLine($"1: Input: root = [1,null,2,3] -> Output: [{string.Join(",", InorderTraversal(root1))}]");

                Console.WriteLine($"2: Input: root = [] -> Output: [{string.Join(",", InorderTraversal(null))}]");

                TreeNode root3 = new TreeNode(1);
                Console.WriteLine($"3: Input: root = [1] -> Output: [{string.Join(",", InorderTraversal(root3))}]\n");
            }

            Console.WriteLine("=== 3 ===");
            {
                Console.WriteLine("Input [\"MinStack\",\"push\",\"push\",\"push\",\"getMin\",\"pop\",\"top\",\"getMin\"] [[],[-2],[0],[-3],[],[],[],[]]");
                MinStack minStack = new MinStack();
                minStack.Push(-2);
                minStack.Push(0);
                minStack.Push(-3);
                int min1 = minStack.GetMin(); 
                minStack.Pop();
                int top1 = minStack.Top();    
                int min2 = minStack.GetMin();
                Console.WriteLine($"Output: [null,null,null,null,{min1},null,{top1},{min2}]\n");
            }

            Console.WriteLine("=== 4 ===");
            {
                Console.WriteLine("Input [\"MyQueue\",\"push\",\"push\",\"peek\",\"pop\",\"empty\"] [[],[1],[2],[],[],[]]");
                MyQueue myQueue = new MyQueue();
                myQueue.Push(1); 
                myQueue.Push(2); 
                int peek1 = myQueue.Peek(); 
                int pop1 = myQueue.Pop();  
                bool empty1 = myQueue.Empty(); 
                Console.WriteLine($"Output: [null,null,null,{peek1},{pop1},{empty1.ToString().ToLower()}]\n");
            }

            Console.WriteLine("=== 5 ===");
            {
                string s1 = "3[a]2[bc]";
                Console.WriteLine($"1: Input: s = \"{s1}\" -> Output: \"{DecodeString(s1)}\"");

                string s2 = "3[a2[c]]";
                Console.WriteLine($"2: Input: s = \"{s2}\" -> Output: \"{DecodeString(s2)}\"");

                string s3 = "2[abc]3[cd]ef";
                Console.WriteLine($"3: Input: s = \"{s3}\" -> Output: \"{DecodeString(s3)}\"\n");
            }

            Console.WriteLine("=== 6 ===");
            {
                string[] tokens1 = { "2", "1", "+", "3", "*" };
                Console.WriteLine($"1: Input: tokens = [\"{string.Join("\",\"", tokens1)}\"] -> Output: {EvalRPN(tokens1)}");

                string[] tokens2 = { "4", "13", "5", "/", "+" };
                Console.WriteLine($"2: Input: tokens = [\"{string.Join("\",\"", tokens2)}\"] -> Output: {EvalRPN(tokens2)}");

                string[] tokens3 = { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" };
                Console.WriteLine($"3: Input: tokens = [\"{string.Join("\",\"", tokens3)}\"] -> Output: {EvalRPN(tokens3)}\n");
            }

            Console.WriteLine("=== 7 ===");
            {
                string s1 = "(()";
                Console.WriteLine($"1: Input: s = \"{s1}\" -> Output: {LongestValidParentheses(s1)}");

                string s2 = ")()())";
                Console.WriteLine($"2: Input: s = \"{s2}\" -> Output: {LongestValidParentheses(s2)}");

                string s3 = "";
                Console.WriteLine($"3: Input: s = \"{s3}\" -> Output: {LongestValidParentheses(s3)}");
            }
        }

        public static bool IsValid(string s)
        {
            if (s.Length < 1 || s.Length > 10000)
                throw new ArgumentException("Довжина рядка має бути в межах [1, 10000].");

            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count == 0) return false;
                    char open = stack.Pop();
                    if (c == ')' && open != '(') return false;
                    if (c == '}' && open != '{') return false;
                    if (c == ']' && open != '[') return false;
                }
            }
            return stack.Count == 0;
        }

        public static IList<int> InorderTraversal(TreeNode? root)
        {
            List<int> result = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode? curr = root;

            int nodeCount = 0; 

            while (curr != null || stack.Count > 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();

                nodeCount++;
                if (nodeCount > 100)
                    throw new ArgumentException("Кількість вузлів перевищує 100.");
                if (curr.val < -100 || curr.val > 100)
                    throw new ArgumentException("Значення вузла має бути в межах [-100, 100].");

                result.Add(curr.val);
                curr = curr.right;
            }

            return result;
        }

        public static string DecodeString(string s)
        {
            if (s.Length < 1 || s.Length > 30)
                throw new ArgumentException("Довжина рядка має бути в межах [1, 30].");

            Stack<int> countStack = new Stack<int>();
            Stack<string> stringStack = new Stack<string>();
            string currentString = "";
            int currentNum = 0;

            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    currentNum = currentNum * 10 + (c - '0');
                }
                else if (c == '[')
                {
                    if (currentNum < 1 || currentNum > 300)
                        throw new ArgumentException("Число k має бути в межах [1, 300].");

                    countStack.Push(currentNum);
                    stringStack.Push(currentString);
                    currentNum = 0;
                    currentString = "";
                }
                else if (c == ']')
                {
                    int repeatTimes = countStack.Pop();
                    StringBuilder decodedString = new StringBuilder(stringStack.Pop());
                    for (int i = 0; i < repeatTimes; i++)
                    {
                        decodedString.Append(currentString);
                    }
                    currentString = decodedString.ToString();
                }
                else
                {
                    currentString += c;
                }
            }

            return currentString;
        }

        public static int EvalRPN(string[] tokens)
        {
            if (tokens.Length < 1 || tokens.Length > 10000)
                throw new ArgumentException("Довжина масиву має бути в межах [1, 10^4].");

            Stack<int> stack = new Stack<int>();

            foreach (string token in tokens)
            {
                if (token == "+" || token == "-" || token == "*" || token == "/")
                {
                    int num2 = stack.Pop();
                    int num1 = stack.Pop();
                    int result = 0;

                    if (token == "+") result = num1 + num2;
                    else if (token == "-") result = num1 - num2;
                    else if (token == "*") result = num1 * num2;
                    else if (token == "/") result = num1 / num2;

                    stack.Push(result);
                }
                else
                {
                    if (int.TryParse(token, out int val))
                    {
                        if (val < -200 || val > 200)
                            throw new ArgumentException("Операнд має бути в межах [-200, 200].");
                        stack.Push(val);
                    }
                    else
                    {
                        throw new ArgumentException($"Невірний токен: {token}");
                    }
                }
            }

            return stack.Pop();
        }

        public static int LongestValidParentheses(string s)
        {
            if (s.Length < 0 || s.Length > 30000)
                throw new ArgumentException("Довжина рядка має бути в межах [0, 3 * 10^4].");

            int maxLength = 0;
            Stack<int> stack = new Stack<int>();
            stack.Push(-1);

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '(' && s[i] != ')')
                    throw new ArgumentException("Рядок має містити лише '(' або ')'.");

                if (s[i] == '(')
                {
                    stack.Push(i);
                }
                else
                {
                    stack.Pop();
                    if (stack.Count == 0)
                    {
                        stack.Push(i);
                    }
                    else
                    {
                        int currentLength = i - stack.Peek();
                        if (currentLength > maxLength)
                        {
                            maxLength = currentLength;
                        }
                    }
                }
            }

            return maxLength;
        }
    }
}