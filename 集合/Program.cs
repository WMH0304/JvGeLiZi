using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace 集合
{
    /*
     * 大多数集合都可在 System.Collections.Generic 和
     * 
     * 集合类型：
     * 1.List  一个存储方式类似于列表的数据集合 
     * 
     * 2.队列  一个存储方式为先进先出的数据集合（把数据看出水，栈是水管）
     * 
     * 3.栈    一个存储方式为先进后出的数据集合（把数据看出水，栈是水桶）
     * 
     * 4.链表  一个存储方式为指向性存储的数据集合
     * 
     * 5.字典   一个存储方式 键 值 的数据集合
     * 
     * 6.集 
     * 
     * 集合可以根据集合类实现的接口组合为列表，集合和字典
     * 
     * IEnumerable<T>  公开枚举数，该枚举数支持在非泛型集合上进行简单迭代。
     * 
     * ICollection<T> 定义操作泛型集合的方法。 为集合提供了 方法: 增（add）,删(clear 或者 remove),查（contains）, 复制（copyto）; 和 属性：获取集合元素数（count），是否标志只读（IsReadOnly）；
     * 
     * IList<T> 表示可按照索引单独访问的一组对象。 IList<T> ：（派生自）ICollection<T> 为集合提供了 属性：获取或设置位于指定索引处的元素（item）；方法：(定义了一个索引器可以根据索引对指定索引的项进行 插入（insert）, 移除（removeat）,获取特定想的索引（indexof）); 
     * 
     * ISet<T> 提供用于集的抽象的基接口。 ISet<T>:ICollection<T>,  IEnumerable<T>, IEnumerable   ISet<T> 由集实现。集允许合并不同的集，获得两个集的交集，检查两个集出否重叠
     * 
     * IDictionary<TKey,TValue> 表示键/值对的泛型集合。 IDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable  包含键和值 的泛型集合类的实现，使用这个接口可以访问索引的键和值，使用见类型的索引器可以访问某些项，还可以添加或删除某些项
     * 
     * ILookup<TKey,TValue> 定义索引器、大小属性以及将键映射到 IEnumerable<T> 值序列的数据结构的布尔搜索方法  ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
     * 
     * IComparer<T> 定义类型为比较两个对象而实现的方法。 IComparer<in T> 可协变接口
     * 
     * IEqualityComparer<T>  定义方法以支持对象的相等比较。 IEqualityComparer<in T> 可协变接口
     * 
     * 
     */

    #region List 列表
    /*
     * List  一个通用的、动态大小的列表数组;泛型类
     * 
     * 1.声明列表是必须给 列表指定类型
     * 2.使用默认的构造函数创建空列表，第一次列表容量扩大为 4 个元素 如果添加了元素的索引大于当前列表的条数 则会将列表的容量重新设置为原来的两倍，
     * 3.
     * 
     * 
     * 
     * 
     * 
     */
     class List_test
    {
        public static void listTest()
        {
            List<int> vs = new List<int>();
            //添加元素
            vs.Add(1);
            vs.Add(2);
            vs.Add(3);
            vs.Add(4);
            List<int> vs1 = new List<int>(7);

            //vs1.Capacity =1;   //设置列表容量

            //vs1 = vs; //当 vs1的容量为 1 时  将vs 赋值给 vs1 他的容量也等于vs 的容量，原因 List是一个通用的、动态大小的列表数组;

            //vs1.AddRange(vs); //这时可以看到 列表的容量为7 而列表的实际容量为 vs.count

            //var intList = new List<int>() { 1, 2, 3, 4, 5, }; //给集合设定初始值初始项

            //vs.Insert(2, 7);//在索引处插入值

            //vs.Remove(2);//在索引处删除元素

            //vs.RemoveRange(2, 3);//删除 第三个，第四个元素

            //vs.Clear();//清空元素

            //vs.IndexOf(2);//查询索引处元素

            //vs.Sort();//排序

           // vs.AsReadOnly();//设置只读


        }
    }


    #endregion

    #region Queue 队列
    class Queue_test
    {
        /*
         * Queue  表示对象的先进先出(fifo)集合。泛型类
         * 
         * 1.Queue是 System.Collections.Generic 中的一个泛型类
         * 2.Queue的继承实现关系：  Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
         * 3.Queue 由于继承了 ICollection 接口 所以他和List 一样支持对自身的增删查改 
         * 4.Queue 数据结构和  List 非常相似  都是以列表的形式存储 有趣的是 Queue 不能在指定索引处插入数据，这个也是队列的特点之一
         * 5.Queue 的容量扩容方式默认情况下和 List 一样 是以 2 的倍数增加（所以呀，一定要限制队列容器的大小呀，不然等数据量大了，他的开销可是很恐怖的）
         * 6.Queue 更加有趣的地方是 他可以在不影响自身数据内容的情况下 遍历自身数据，这个特点和线程搭配使用简直不要太爽。
         * 7.和线程搭配使用的时候记得加线程锁。。。。
         * 
         * Queue 支持的方法
         * 
         * 1.Clear  从 Queue<T> 中移除所有对象。
         * 2.Contains 确定某元素是否在 Queue<T> 中。
         * 3.CopyTo  从指定数组索引开始将 Queue<T> 元素复制到现有一维 Array 中。
         * 4.Dequeue  移除并返回位于 Queue<T> 开始处的对象。
         * 5.Enqueue  将对象添加到 Queue<T> 的结尾处。
         * 6.Equals(Object) 确定指定的对象是否等于当前对象。 （继承自 Object。）
         * 7.Finalize  在垃圾回收将某一对象回收前允许该对象尝试释放资源并执行其他清理操作。 （继承自 Object。）
         * 8.GetEnumerator 返回循环访问 Queue<T> 的枚举数。
         * 9.MemberwiseClone 创建当前 Object 的浅表副本。 （继承自 Object。）
         * 10.Peek 返回位于 Queue<T> 开始处的对象但不将其移除。
         * 11.TrimExcess  如果元素数小于当前容量的 90%，将容量设置为 Queue<T> 中的实际元素数。
         * 
         */

        public static void Queue_tests()
        {
            Queue<string> numbers = new Queue<string>();
            //Enqueue 将对象添加到 Queue<T> 的结尾处。
            numbers.Enqueue("ubs");

            numbers.Peek();// Queue<T> 开始处的对象但不将其移除
            Console.WriteLine(numbers.Peek());

            numbers.Contains("ubs");//队列中是否含有该元素
            Console.WriteLine(numbers.Contains("ubs"));

            //numbers.Dequeue();//移除并返回队列开始处对象
            Console.WriteLine(numbers.Dequeue());


            numbers.Enqueue("one");
            numbers.Enqueue("two");
            numbers.Enqueue("three");
            numbers.Enqueue("four");
            numbers.Enqueue("five");



            foreach (string item in numbers)
            {
                Console.WriteLine(item);
            }
        }

      

    }

    /*
     * 举个粒子
     */
    #region 使用线程读取队列中的数据

   
    public class Document
    {
        public string Title { get; private set; }

        public string Content { get; private set; }

        public  byte Priority { get; private set; }

        public Document(string t, string c)
        {
            Title = t;
            Content = c;
        }
        /// <summary>
        /// 链表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="priority"></param>
        public Document(string title,string content,byte priority)
        {
            Title = title;
            Content = content;
            Priority = priority;
        }



    }

    //操作队列
    public class DocumentManage
    {
        private readonly Queue<Document> _documentsQueue = new Queue<Document>();

        //将文档添加到队列中
        public void AddDocument(Document doc)
        {
            //lock 关键字将语句块标记为临界区，方法是获取给定对象的互斥锁，执行语句，然后释放该锁
            lock (this)
            {
                _documentsQueue.Enqueue(doc);
            }
        }

        //在队列中读取文档
        public Document GetDocument()
        {
            Document document = null;
            lock (this)
            {
                document = _documentsQueue.Dequeue();
            }
            return document;
        }

        //队列是否存在数据
        public bool IsDocumentAvailable => _documentsQueue.Count > 0;

    }


    public class ProcessDocuments
    {
        public static void StartAsync(DocumentManage dm)
        {
            //开启线程
            Task.Run(new ProcessDocuments(dm).Run);
        }
        //给实体赋值
        protected ProcessDocuments(DocumentManage dm)
        {
            //if (dm==null)
            //{
            //    throw new ArgumentNullException("dm");
            //}
            //_documentManager = dm;
            _documentManager = dm ?? throw new ArgumentNullException("dm");
        }

        //实体对象
        private DocumentManage _documentManager;
       
        //异步方法
        protected async Task Run()
        {
            while (true)
            {
                //判断 _documentManager 是否存在事件
                if (_documentManager.IsDocumentAvailable)
                {
                    //读取数据
                    Document doc = _documentManager.GetDocument();
                    //输出 读取到的数据
                    Console.WriteLine("读取到的数据   {0}", doc.Title);
                }
                //延迟后执行
                await Task.Delay(new Random().Next(20));
            }
        }

    }

    #endregion
    #endregion

    #region Stack 栈
    /*
     * 
     * Stack<T> 类  表示可变大小的后进先出 (LIFO) 集合（对于相同指定类型的实例）。泛型类
     * 
     * 1.Stack System.Collections.Generic 中的一个泛型类
     * 2.Stack 的继承实现关系： Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
     * 3.Stack 是一个后进先出的容器（砌墙的石头————都来者居上）
     * 4.Stack 和  Queue 非常相似 唯一的不同点就是 他们读取数据的方式的不同
     * 
     */
     class StaskTest
    {
      static  public void StaskTests()
        {
            var ap = new Stack<string>();
            ap.Push("one");
            ap.Push("two");
            ap.Push("three");


            Console.WriteLine(ap.Pop());
            Console.WriteLine(ap.Peek());
        }
    }



    #endregion

    #region LinkedList<T> 双向链表
    /*
     *  LinkedList<T> 表示双向链接列表。泛型类
     *  
     * 1.LinkedList 继承实现关系: LinkedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection
     * 2.LinkedList 双向链表 ，列表中的每一个元素都指向他前面和后面的元素
     * 3.LinkedList 每个元素都有三个模块  Value Next Previous  
     * 3.1 Value： 元素的值  
     * 3.2 Next ： 指向下一个元素引用，（这有点像指针（这个模块存储的是下一个元素的地址））
     * 3.3 Previous ：指向上一个元素引用
     * 4.LinkedList  将元素插入列表的中间位置时，速度最快，因为在插入数据时只需要修改上一个元素的（next）引用和下一个元素的（Previous）引用
     * 5.LinkedList  查询效率最低 原因：因为链表的存储结构是 每个元素是以引用（地址）的方式联系的，所以在列表中 只有相邻的两个元素知道彼此的地址列表的元素只能一个一个元素访问，这就需要比较长的时间来访问第一个，或者最后一个元素
     * 6.LinkedList  通俗的说，在链表中只有相邻的两个节点知道彼此的地址（逻辑地址）
     * 7.LinkedList  支持的属性
     * 7.1 Count 获取 LinkedList<T> 中实际包含的节点数。
     * 7.2 First 获取 LinkedList<T> 的第一个节点。 
     * 7.3 Last  获取 LinkedList<T> 的最后一个节点。 
     * 8. LinkedList 数据结构和 List 数据结构极其相似 只不过list 是一个大的存储块，然后数据的相对地址和逻辑地址都是相邻的 而由于链表的每个元素都有上一个和下一个元素的逻辑地址所以链表可以是很多个内存块
     *  
     */

    #region 链表声明理解
        public class GenericCollection
    {

        public static void LineList_Main()
        {
            //一个节点列表
            LinkedListNode<string> lln = new LinkedListNode<string>("one");
            WriteLine("创建节点");
            DisplayProperties(lln);
            //创建一个链表
            LinkedList<string> ll = new LinkedList<string>();
            //添加节点one
            ll.AddLast(lln);
            Console.WriteLine("将节点添加到空链表后……");
            DisplayProperties(lln);
            ll.AddFirst("0"); // 在当前节点前插入节点
            ll.AddLast("two");// 在当前节点后插入节点
            Console.WriteLine("加入0和two");
            DisplayProperties(lln);


        }




        public static void DisplayProperties(LinkedListNode<String> lln)
        {
            //节点数
            if (lln.List == null)
                Console.WriteLine("节点未链接。");
            else
                Console.WriteLine("节点属于包含{0}元素的链表。", lln.List.Count);
            //上一个节点
            if (lln.Previous == null)
                Console.WriteLine("前一个节点为空。");
            else
                Console.WriteLine("前一个节点的值:{0}", lln.Previous.Value);

            Console.WriteLine("当前节点的值:{0}", lln.Value);

            //下一个节点
            if (lln.Next == null)
                Console.WriteLine("下一个节点是null。");
            else
                Console.WriteLine("下一个节点的值:{0}", lln.Next.Value);

            Console.WriteLine();
        }
    }
    #endregion

    /// <summary>
    /// 以链表的形式存储文档类型
    /// </summary>
    public class PriorityDocumentManager
    {
        //声明一个 Document 类型的链表
        private readonly LinkedList<Document> _documentList;

        // priorities 0.9  优先权 声明子节点

        /*
         * LinkedListNode<Document> 获得列表中的上一个 和下一个元素
         * 
         * List 获取 LinkedListNode<T> 所属的 LinkedList<T>。
         * Next 获取 LinkedList<T> 中的下一个节点 
         * Previous 获取 LinkedList<T> 中的上一个节点。 
         * Value 获取节点中包含的值。 
         * 
         */
        private readonly List<LinkedListNode<Document>> _priorityNodes;

        public PriorityDocumentManager()
        {
            //声明一个链表
            _documentList = new LinkedList<Document>();

            // LinkedListNode<T> 类  表示 LinkedList<T> 中的节点。此类不能被继承。
            _priorityNodes = new List<LinkedListNode<Document>>(10);
            for (int i = 0; i < 10; i++)
            {
                _priorityNodes.Add(new LinkedListNode<Document>(null));
            }
        }

        public void AddDocument(Document d)
        {
            // Contract.Requires<ArgumentNullException>(d != null, "argument d must not be null");
            //  if (d == null) throw new ArgumentNullException("d");

            AddDocumentToPriorityNode(d, d.Priority);
        }

        private void AddDocumentToPriorityNode(Document doc, int priority)
        {
            //   Contract.Requires<ArgumentException>(priority >= 0 && priority < 10, "priority value must be between 0 and 9");
            //if (priority > 9 || priority < 0)
            //    throw new ArgumentException("Priority must be between 0 and 9");

            if (_priorityNodes[priority].Value == null)
            {
                --priority;
                if (priority >= 0)
                {
                    // check for the next lower priority 检查下一个较低的优先级
                    AddDocumentToPriorityNode(doc, priority);
                }
                else // now no priority node exists with the same priority or lower 现在不存在具有相同或更低优先级的节点
                     // add the new document to the end 将新文档添加到末尾
                {
                    _documentList.AddLast(doc);
                    _priorityNodes[doc.Priority] = _documentList.Last;
                }
                return;
            }
            else // a priority node exists  存在优先级节点
            {
                LinkedListNode<Document> prioNode = _priorityNodes[priority];
                if (priority == doc.Priority)
                // priority node with the same priority exists 存在具有相同优先级的优先节点
                {
                    _documentList.AddAfter(prioNode, doc);

                    // set the priority node to the last document with the same priority 将优先级节点设置为具有相同优先级的最后一个文档
                    _priorityNodes[doc.Priority] = prioNode.Next;
                }
                else // only priority node with a lower priority exists 只有优先级较低的节点存在
                {
                    // get the first node of the lower priority 获得优先级较低的第一个节点
                    LinkedListNode<Document> firstPrioNode = prioNode;

                    while (firstPrioNode.Previous != null &&
                       firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = prioNode.Previous;
                        prioNode = firstPrioNode;
                    }

                    _documentList.AddBefore(firstPrioNode, doc);

                    // set the priority node to the new value 将优先级节点设置为新值
                    _priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }
         }

        //打印链表数据
        public void DisplayAllNodes()
        {
            foreach (Document doc in _documentList)
            {
                WriteLine($"priority: {doc.Priority}, title {doc.Title}");
            }
        }

        // returns the document with the highest priority 返回优先级最高的文档
        // (that's first in the linked list) (它是链表中的第一个)
        public Document GetDocument()
        {
            Document doc = _documentList.First.Value;
            _documentList.RemoveFirst();
            return doc;
        }

    }




    #endregion

    #region 有序列表

    #region  SortedList<TKey, TValue>
    /*
    * SortedList  表示根据键进行排序的键/值对的集合，而键基于的是相关的 IComparer<T> 实现
    * 
    * 1.SortedList<TKey,TValue>  TKey：集合中的键的类型  TValue : 集合中值的类型
    * 2.SortedList  SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    * 3.SortedList 每个键都必须是唯一的且不能为null 如果列表内存在象提的键值（索引） add 就会抛出一个argument Exception 的异常
    * 4.SortedList 数据结构 是以键值对的方式存储到集合中，这个和链表的额存储方式有点像是（可以说他是list 和 linkedlist 的结合体）
    *  
    *  
    * Lookup<TKey, TElement> 类 表示映射到一个或多个值的各个键的集合。
    * 
    * 1. Lookup 是 SortedList 的改良版
    * 2. Lookup 支持集合存放多个相同的键
    * 3. Lookup 所属空间是   System.Linq 
    * 
    * 
    * 
    * 
    */
    class SortedLists
    {
      class Test_data
        {
            string a;
            string b;
            string c;
        }
        public static void SortedLists_main()
        {
            SortedList<int, string> sl = new SortedList<int, string>();
            sl.Add(1, "1");
            sl.Add(2, "1");
            sl.Add(3, "3");
            sl.Add(4, "4");

           
        }
    }





    #endregion




    #endregion

    #region 字典（映射表和散列表）

    /*
     * 字典 
     * 
     * 字典允许按照某个键来访问元素，所以他又叫 映射表或散列表
     * 
     * 字典的特性是能根据键快速查找值，也可以自由添加和删除元素，这一点和list 十分相似，但散列表却没有在内存中移动后续元素的开销。
     * 
     * 
     * 
     * 
     * 
     */
   
    public class Dictionarys
    {
        public static void Dictionarys_main()
        {
            //实例化一个字典类
            var dict = new Dictionary<int, string>()
            {
                [1] = "one",
                [3] ="three"
            };
            dict.Add(2,"22");

            foreach (var item in dict)
                Console.WriteLine("    "+ item.Key + "    " + item.Value +dict.Count );


        }
    }





    #endregion


    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {
            #region List
            ///List_test.listTest(); //List
            #endregion

            #region Queue
            // Queue_test.Queue_tests();//Queue

            #region 使用线程读取队列中的数据
            //var dm = new DocumentManage();
            ////调用这个方法，然后开启一个线程
            //ProcessDocuments.StartAsync(dm);
            ////创建文档并将它们添加到DocumentManager
            //for (int i = 0; i < 100; i++)
            //{
            //    //想实体类传数据
            //    Document doc = new Document("Doc " + i.ToString(), "content");
            //    //将数据添加到队列中
            //    dm.AddDocument(doc);
            //    //循环生成的数据
            //    WriteLine(" 循环生成的数据  {0}", doc.Title);
            //    // Delay 创建将在时间延迟后完成的任务。
            //    // Next 返回一个小于所指定最大值的非负随机整数
            //    // Wait -等待 Task 完成执行过程。
            //    Task.Delay(new Random().Next(20)).Wait();
            //}
            #endregion

            #endregion

            #region Stack
            //StaskTest.StaskTests(); 
            #endregion

            #region LinkedList
            #region 文件链表


            //PriorityDocumentManager pdm = new PriorityDocumentManager();
            //pdm.AddDocument(new Document("one", "Sample", 8));
            //pdm.AddDocument(new Document("two", "Sample", 3));
            //pdm.AddDocument(new Document("three", "Sample", 4));
            //pdm.AddDocument(new Document("four", "Sample", 8));
            //pdm.AddDocument(new Document("five", "Sample", 1));
            //pdm.AddDocument(new Document("six", "Sample", 9));
            //pdm.AddDocument(new Document("seven", "Sample", 1));
            //pdm.AddDocument(new Document("eight", "Sample", 1));
            //pdm.DisplayAllNodes();

            #endregion

            //GenericCollection.LineList_Main();

            #endregion

            #region 有序列表
            #endregion

            #region 字典



            Dictionarys.Dictionarys_main();


            #endregion

            ReadLine();
        }
    }
    #endregion












































































































    /**************   **********************/
}
