using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static 集合.Employee;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.IO;

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
     * 6.集  set 分集内元素不重复的有序集 和 集内元素不重复的无序集
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

    #region Dictionary 字典（映射表和散列表）

    /*
     * Dictionary 字典 
     * 
     * Dictionary 字典允许按照某个键来访问元素，所以他又叫 映射表或散列表
     * 
     * Dictionary 字典的特性是能根据键快速查找值，也可以自由添加和删除元素，这一点和list 十分相似，但散列表却没有在内存中移动后续元素的开销。
     * 
     * Dictionary 字典在插入数据时
     * 
     * Dictionary 字典中的类型必须重写object 类中的 GetHashCode()方法。只要字典类确定元素位置
     * GetHashCode()方法 的实现代码必须满足以下条件
     * 1.相同对象应返回相同的值
     * 2.不同对象可以返回相同的值
     * 3.不能抛出异常
     * 4.至少使用一个实例字段
     * 5.散列代码最好在对象的生存期中不发生变化
     * 6.执行较快，计算开销不大
     * 7.散列代码值应平 均分布在 int 可以存储的整个数字范围上
     * 
     * 
     * 为什么散列代码值应平均分布在 int 呢？
     * 如果两个键返回的散列码 （就是执行 gethascode 时返回的 哈希码
     * 
     * 
     * 
     * note：字典的性能取决于 GetHashCode() 方法的实现
     * 
     * 
     */
        public class Employee
    {
        private string _name;
        private decimal _salary;
        private readonly EmployeeId _id;

        public Employee(EmployeeId id, string name, decimal salary)
        {
            _id = id;
            _name = name;
            _salary = salary;
        }
        public class EmployeeIdException : Exception
        {
            public EmployeeIdException(string message) : base(message) { }
        }

        public struct EmployeeId : IEquatable<EmployeeId>
        {
            private readonly char _prefix;
            private readonly int _number;

            public EmployeeId(string id)
            {
                if (id == null) throw new ArgumentNullException(nameof(id));

                _prefix = (id.ToUpper())[0];
                int numLength = id.Length - 1;
                try
                {
                    _number = int.Parse(id.Substring(1, numLength > 6 ? 6 : numLength));
                }
                catch (FormatException)
                {
                    throw new EmployeeIdException("Invalid EmployeeId format");
                }
            }

            public override string ToString() => _prefix.ToString() + $"{_number,6:000000}";

            public override int GetHashCode() => (_number ^ _number << 16) * 0x15051505;

            public bool Equals(EmployeeId other) => (_prefix == other._prefix && _number == other._number);

            public override bool Equals(object obj) => Equals((EmployeeId)obj);

            public static bool operator ==(EmployeeId left, EmployeeId right) => left.Equals(right);

            public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
        }


        public override string ToString() => $"{_id.ToString()}: {_name, -20} {_salary :C}";

    }
        

       

    public class Dictionarys
    {
        public static void Dictionarys_main()
        {
            var idTony = new EmployeeId("C3755");
            var tony = new Employee(idTony, "Tony Stewart", 379025.00m);

            var idCarl = new EmployeeId("F3547");
            var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);

            var idKevin = new EmployeeId("C3386");
            var kevin = new Employee(idKevin, "Kevin Harwick", 415261.00m);

            var idMatt = new EmployeeId("F3323");
            var matt = new Employee(idMatt, "Matt Kenseth", 1589390.00m);

            var idBrad = new EmployeeId("D3234");
            var brad = new Employee(idBrad, "Brad Keselowski", 322295.00m);

            var employees = new Dictionary<EmployeeId, Employee>(31)
            {
                [idTony] = tony,
                [idCarl] = carl,
                [idKevin] = kevin,
                [idMatt] = matt,
                [idBrad] = brad
            };

            foreach (var employee in employees.Values)
            {
                WriteLine(employee);
            }

            while (true)
            {
                Write("Enter employee id (X to exit)> ");
                var userInput = ReadLine();
                userInput = userInput.ToUpper();
                if (userInput == "X") break;

                EmployeeId id;
                try
                {
                    id = new EmployeeId(userInput);


                    Employee employee;
                    if (!employees.TryGetValue(id, out employee))
                    {
                        WriteLine("Employee with id {0} does not exist", id);
                    }
                    else
                    {
                        WriteLine(employee);
                    }
                }
                catch (EmployeeIdException ex)
                {
                    WriteLine(ex.Message);
                }
            }




            ////实例化一个字典类
            //var dict = new Dictionary<int, string>()
            //{
            //    [1] = "one",
            //    [3] ="three"
            //};
            //dict.Add(2,"22");

            //foreach (var item in dict)
            //    Console.WriteLine("    "+ item.Key + "    " + item.Value +dict.Count );


        }
    }





    #endregion

    #region LookUp类
    /*
     * LookUp  表示映射到一个或多个值的各个键的集合。
     * 
     * LookUp 和dictionary 非常相似都是以键值对的形式存储数据
     * 
     * LookUp 实际上就将键映射到一个值集合上 ，
     * 
     * lookup 在 system.core 中实现 ，用system.linq 命名空间定义
     * 
     * 有趣的是 lookup 并并不能像 dictionary 那样直接调用 ， 需要调用 ToLookup()方法，
     * 
     * 这个是快速查询的之中方式
     */
    public class Racer : IComparable<Racer>, IFormattable
    {
        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Wins = wins;
        }

        public Racer(int id, string firstName, string lastName, string country)
          : this(id, firstName, lastName, country, wins: 0)
        { }

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Wins { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";


        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "N";
            switch (format.ToUpper())
            {
                case null:
                case "N": // name
                    return ToString();
                case "F": // first name
                    return FirstName;
                case "L": // last name
                    return LastName;
                case "W": // Wins
                    return $"{ToString()}, Wins: {Wins}";
                case "C": // Country
                    return $"{ToString()}, Country: {Country}";
                case "A": // All
                    return $"{ToString()}, {Country} Wins: {Wins}";
                default:
                    throw new FormatException(String.Format(formatProvider,
                          "Format {0} is not supported", format));
            }
        }

        public string ToString(string format) => ToString(format, null);

        public int CompareTo(Racer other)
        {
            if (other == null) return -1;
            int compare = string.Compare(LastName, other.LastName);
            if (compare == 0)
                return string.Compare(FirstName, other.FirstName);
            return compare;
        }
    }


    #endregion


    #region 有序字典 SortedDictionary

    /*
     * SortedDictionary 有序字典   表示根据键进行排序的键/值对的集合。
     *  
     * SortedDictionary 二叉搜索树 根据期元素根据键进行排序 实现了
     * 
     * SortedDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>,ICollection, IDictionary
     * 
     * SortedDictionary 如果键的类型不能排序,则还可以创建一个实现 ICo,parer<TKey>接口的比较强，将比较强用作有序字典的构造函数的一个参数
     * 
     * SortedDictionary 和 sortedlist 及其相似 只不过  sortedlist 实现一个数组的列表 ，而 SortedDictionary 实现一个字典
     * 
     * SortedDictionary 和 sortedlist 的比较
     * 1.SortedDictionary 使用的内存比 sortedlist 大
     * 2.SortedDictionary 的元素的插入和删除操作比较快（原因是他借鉴了链表的存储结构）
     * 3.在用已排好需的数据填充集合是，不需要修改容量  sortedlist 比较快 （原因是他的存储结构借鉴了list,所以他本身也具备了list的一些特性）
     * 
     * 
     */
    #endregion

    #region 集
    /* 
     * 集  可以包含不重复元素的集合成为“集”
     * 
     * .net 中包含了两个集 （Hash<T> 和SortedSet<T> 他们的区别在于 hash 包含不重复元素的无序列表 ，而SortedSet 则是包含了不重复的有序列表）
     * 
     * 他们实现了 ISet<T> 接口 （提供可以创建合集，交集，或者给出一个集和另一个集的超集或子集）
     * 
     * 
     * 
     * 
     * 
     * 
     */
    #endregion

    #region 性能

    /*
     * 前面讲诉的集合类中，有好个集合的功能都是一样的，当时正纳闷呢，
     * 这两个集合（sortedlist 和 sorteddictionary）能做的事情是一样的，为什么还要多次一举呢?
     * 现在知道了 其根本原因就是性能的差异
     * 
     * 例如：
     *  sortedlist 使用的内存少（时间换空间）
     *  sorteddictionary 检索速度快（空间换时间）
     *  
     *  操作时间用大写的 O 记号
     *  1. O(1)      表示无论集合中有多少数据，操作需要的时间不变  栗子 array list add方法具有 O(1)行为
     *  
     *  2. O(log n)  表示操作需要的使劲按随集合中元素增加而增加但每个元素所需要的时间时对数曲线 ，在执行插入操作时
     *  
     *  
     *  3. O(n)  表示 对于集合执行一个操作需要的时间在最坏情况下时时 N , 如果需要重写给集合分配内存， 
     *  arraylist 的 add 方法就是一个 O(n)操作 改变容量，需要复制列表，
     *  复制时间随元素的增加而增加 sorteddictionary 具有 O(log n) 行为 而sortedlist 的时间复杂度时 O(n)
     *  原因时 sorteddictionary 是树形结构，在树形结构中插入元素的效率要比 列表高很多
     *  
     *  
     *  各集合的各操作的时间复杂度图片在
     *  image 文件夹里面
     *  
     * 
     *  
     *  
     */



    #endregion

    #region 小结

    /*
     * list 一个可以动态增长的数组列表
     * 
     * queue 一个以先进先出的方式访问元素的集合
     * 
     * stack 一个以先进后出的方式访问元素的集合
     * 
     * linkedlist 一个已元素指向的值访问元素（搜索操作较慢），但可以快速插入和删除元素的集合
     * 
     * dictionary 通过 键值对的方式存储数据的集合 ，搜索和插入操作都比较快（可以说是链表和列表的结合体）
     * 
     * set  一个以无重复项作为最大卖点的集合，可分为 hashset (无序列表) 和 sortedset(有序列表)
     * 
     */
    #endregion


    /*》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》*/

    #region BitArray
    /*
     * BitArray 点阵列 ： 一个紧凑型的位置数组，使用布尔值表示，其中 true 表示位是开始的（1），false 表示位是关闭的（0）
     * 说明文档介绍
     * (管理位值的压缩数组，该值表示为布尔值，其中 true 表示位是打开的 (1)，false 表示位是关闭的 (0)。)
     * 
     * BitArray 点阵列 ： 位于 system.collections 中 是一个引用类型，包含一个 int 数组 ，其中每 32  位使用一个新整数
     * 
     * BitArray 点阵列 ：  BitArray : ICollection, IEnumerable, ICloneable
     * 
     * BitArray 点阵列 ： 成员
     * 1.and  针对指定的 BitArray 中的相应元素对当前 BitArray 中的元素执行按位“与”运算。
     * 2.
     * 
     * 
     * 
     * 
     */

    class BitArrays
    {
        public static void BitArrays_main()
        {
            BitArray bit1 = new BitArray(4);
            BitArray bit2 = new BitArray(4);
            BitArray bit3 = new BitArray(4);

            bit1[0] = bit2[0] = false;
            bit1[1] = bit2[1] = true;
            bit1[2] = bit2[2] = false;
            bit1[3] = bit2[3] = true;

            bit3[0] = true;
            bit3[1] = false;
            bit3[2] = false;
            bit3[3] = false;


            //.And 与
            Console.WriteLine(bit1.And(bit2).Count);
            PrintValues(bit1, bit1.Count);
            //.Xor 异或  如果a、b两个值不相同，则异或结果为1。如果a、b两个值相同，异或结果为0。
            Console.WriteLine(bit1.Xor(bit3).Count);
            PrintValues(bit1, bit1.Count);
            //.Not 反转所有位值
            bit2.Not();
            PrintValues(bit2, bit2.Count);

            //修改指定位置值
            bit2.Set(1,true);
            PrintValues(bit2, bit2.Count);
            //批量修改值
            bit3.SetAll(true);
            PrintValues(bit3, bit2.Count);

            //获取指定索引处的值
            Console.WriteLine(bit3.Not().Get(0));


        }
        public static void PrintValues(IEnumerable my,int myl)
        {
            int i = myl;
            foreach (Object obj in my)
            {
                if (i <= 0)
                {
                    i = myl;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }


    }

    #endregion

    #region BitVector32
    /*
     * 
     * BitVector32 提供一个简单结构，该结构以 32 位内存存储布尔值和小整数。
     * 
     * BitVector32 功能和 BitArray 一样 但 BitVector32 是值类型 必须给其赋值（位数值）而 BitArray 可以动态添加位数值（类似于list）
     */

        class BitVector32s
    {
        public static void BitVector32s_main()
        {
            //使用CreateMask方法创建一个遮罩
            // create a mask using the CreateMask method
            var bits1 = new BitVector32();
            int bit1 = BitVector32.CreateMask();
            int bit2 = BitVector32.CreateMask(bit1);
            int bit3 = BitVector32.CreateMask(bit2);
            int bit4 = BitVector32.CreateMask(bit3);
            int bit5 = BitVector32.CreateMask(bit4);

            bits1[bit1] = true;
            bits1[bit2] = false;
            bits1[bit3] = true;
            bits1[bit4] = true;
            bits1[bit5] = true;
            WriteLine(bits1);

            // create a mask using an indexer  使用索引器创建掩码
            bits1[0xabcdef] = true;
            WriteLine(bits1);

            int received = 0x79abcdef;

            BitVector32 bits2 = new BitVector32(received);
            WriteLine(bits2);

            // sections: FF EEE DDD CCCC BBBBBBBB
            // AAAAAAAAAAAA
            BitVector32.Section sectionA = BitVector32.CreateSection(0xfff);
            BitVector32.Section sectionB = BitVector32.CreateSection(0xff, sectionA);
            BitVector32.Section sectionC = BitVector32.CreateSection(0xf, sectionB);
            BitVector32.Section sectionD = BitVector32.CreateSection(0x7, sectionC);
            BitVector32.Section sectionE = BitVector32.CreateSection(0x7, sectionD);
            BitVector32.Section sectionF = BitVector32.CreateSection(0x3, sectionE);

            WriteLine($"Section A: {IntToBinaryString(bits2[sectionA], true)}");
            WriteLine($"Section B: {IntToBinaryString(bits2[sectionB], true)}");
            WriteLine($"Section C: {IntToBinaryString(bits2[sectionC], true)}");
            WriteLine($"Section D: {IntToBinaryString(bits2[sectionD], true)}");
            WriteLine($"Section E: {IntToBinaryString(bits2[sectionE], true)}");
            WriteLine($"Section F: {IntToBinaryString(bits2[sectionF], true)}");
        }
        static string IntToBinaryString(int bits, bool removeTrailingZero)
        {
            var sb = new StringBuilder(32);

            for (int i = 0; i < 32; i++)
            {
                if ((bits & 0x80000000) != 0)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
                bits = bits << 1;
            }

            string s = sb.ToString();
            if (removeTrailingZero)
            {
                return s.TrimStart('0');
            }
            else
            {
                return s;
            }
        }

    }


    #endregion

    #region 可观察集合  ObservableCollection<T>
    /*
     *  ObservableCollection 表示一个动态数据集合，在添加项、移除项或刷新整个列表时，此集合将提供通知。
     *  
     *  之前做 WPF 的时候用过这个集合，用于动态显示 datagrid 数据，啊哈，现在又看到你了
     *  
     *  ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
     *  
     *  ObservableCollection 派生自 collection （用于创建集合的类，上面的很多集合都派生自他）基类 ，collection 可以自定义集合 ，并在内部使用 List<T>类
     *  
     *  ObservableCollection 重写了 collection 中的虚方法 setitem 和 removeitem 用以触发 CollectionChanged 事件（当集合更改时发生）
     *  
     *  ObservableCollection 可以使用 INotifyCollectionChanged (向侦听器通知动态更改，如在添加或移除项时或在刷新整个列表时。)接口 注册 CollectionChanged 事件
     *  
     *  ObservableCollection 由于继承了 collection 所以 ObservableCollection 也支持 collection 的一系列的方法 
     *  
     *  ObservableCollection 总的来说 就是一个声明了当集合改变时发生的事件的动态显示集合（不知道是否也具有这个特性）
     *  
     */

    class Oc_data
    {
        public int one { get; set; }

        public string two { get; set; }

        public string three { get; set; }

    }
     

    class ObservableCollections 
    {
        public static void ObservableCollection_main()
        {
            ObservableCollection<Oc_data> oc_Datas = new ObservableCollection<Oc_data>();
            oc_Datas.CollectionChanged += Data_CollectionChanged;
            for (int i = 0; i < 10; i++)
            {
                //向动态集合中填充数据
                oc_Datas.Add(new Oc_data() { one = 1, two = "1", three = "1" });
            }
            //移除指定索引项
            oc_Datas.Remove(oc_Datas[oc_Datas.Count-1]);
            oc_Datas.Insert(oc_Datas.Count, new Oc_data() { one = 3, two = "3", three = "3" });
            foreach (var item in oc_Datas)
            {
                Console.Write(item.one +"  "+item.two + "  " + item.three);
                Console.WriteLine();
            }

            oc_Datas.CollectionChanged -= Data_CollectionChanged;

            var data = new ObservableCollection<string>();
            //事件： CollectionChanged 在添加、删除、更改、移动项或整个列表时发生
            data.CollectionChanged += Data_CollectionChanged;
            
            /* 
             * 
             * 当集合的数据发生改变时，调用委托绑定的方法
             * 
             */

            data.Add("One");
            data.Add("Two");
            data.Insert(1, "Three");
            data.Remove("One");
            //撤销委托
            data.CollectionChanged -= Data_CollectionChanged;
            ReadLine();
        }

        /// <summary>
        /// 受委托的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            WriteLine($"action: {e.Action.ToString()}");

            if (e.OldItems != null)
            {
                WriteLine($"旧项的起始索引: {e.OldStartingIndex}");
                WriteLine("old item(s):");
                foreach (var item in e.OldItems)
                {
                    WriteLine(item);
                }
            }
            if (e.NewItems != null)
            {
                WriteLine($"新项目的起始索引: {e.NewStartingIndex}");
                WriteLine("new item(s): ");
                foreach (var item in e.NewItems)
                {
                    WriteLine(item);
                }
            }
            WriteLine();
        }
    }



    #endregion

    #region 不变的集合
    /*
     * 如果对象可以改变其状态,就很难在多个同时运行的任务中使用。
     * 但是如果对象不能改变其状态，就很容易在多线程中使用，不能改变的对象称之为不变的对象。不能改变的集合称之为不变的对象
     * 
     * 引用nuget 包： system.collection.immutable 
     * 这个包提供线程安全的集合，并且保证不会更改其内容，
     * 也称为不可变集合。与字符串一样，执行修改的任何方法都不会更改现有实例，
     * 而是返回一个新实例。出于效率考虑，
     * 该实现使用共享机制来确保新创建的实例与前一个实例共享尽可能多的数据，
     * 同时确保操作具有可预测的时间复杂度。
     *  
     *  ImmutableArray<T> 提供了添加元素的Add()方法。但是，与其他集合相反，Add()方法不会改变不变集合本身而是返回一个新的不变集合
     * 
     * 
     * 不变的集合 支持 linq 查询 
     *  
     */
    class data_Immutables
    {
        public string Name { get; }
        public decimal Amount { get; }
        public data_Immutables(string nae, decimal aunt)
        {
            Name = nae;
            Amount = aunt;
        }
    }
    class Immutables
    {
      

        public  static void Immutabless()
        {
            //创建一个空数组
            ImmutableArray<string> ia = ImmutableArray.Create<string>();
            //由于 ImmutableArray 并不改变自身而是返回一个新的实例，所以 ia 仍然为空
            //而 ia1 则 是包含一个元素   code 的不变集合（由 add() 返回, 可以连续添加）
            ImmutableArray<string> ia1 = ia.Add("code").Add("read");

            var data_Immutables1 = new List<data_Immutables>()
            {
                new data_Immutables("11111",333333m),
                new data_Immutables("22222",222222m),
                new data_Immutables("33333",111111m)
            };
            //引用 System.Collections.Immutable; 使用 ToImmutableList 方法创建一个不变的集合（需要 nuget 引用）
            //转到定义一看他实现了一大串接口。。。由于实现了 IEnumerable 接口，所以他可以向其它集合那样枚举，只是他自身不能改变（自身不该变是不变集合的本质）
            ImmutableList<data_Immutables> data_s = data_Immutables1.ToImmutableList();

            foreach (var item in data_s)
            {
                Console.WriteLine(item.Name +"  "+ item.Amount);
            }
            //ImmutableList 自身也定义了foreach 方法（这个方法定义了以个Action<T> 泛型委托作为参数） 可以和 lambda 一起使用 
            data_s.ForEach(a => WriteLine($"{a.Name} {a.Amount}"));
            /*
             
            由于不变几个最大的卖点就是集合的状态不变，
            所以 ImmutableList 内类似的其它集合的方法功能都一样，
            唯一的区别就是 ImmutableList 返回的是一个新的不变集合

            */
            ImmutableList<data_Immutables>.Builder  data_s1 = data_s.ToBuilder();

            //遍历新集合
            for (int i = 0; i < data_s1.Count; i++)
            {
                data_Immutables a = data_s1[i];
                if (a.Amount > 0)
                {
                    data_s1.Remove(a);
                }
            }
            // ToImmutable 基于此实例的内容创建一个不可变列表。
            ImmutableList<data_Immutables> overdrawnAccounts = data_s1.ToImmutable();

            overdrawnAccounts.ForEach(a => WriteLine($"{a.Name} {a.Amount}"));

            // ImmutableArray 第一个是一个结构体 ImmutableArray 表示不可变数组;也就是说一旦改变就不能再改变了 创建。NuGet包: System.Collections。不可变(关于不可变集合以及如何安装)

            //ImmutableArray 第二个是一个类 ImmutableArray  提供用于创建不可变数组的方法;这意味着一旦它被创建，就不能被更改。NuGet包:System.Collections。不可变(关于可变集合和如何安装)
            ImmutableArray<string> arr = ImmutableArray.Create<string>("one", "two", "three", "four", "five");
            //StartsWith 如果值匹配此字符串的开头，则为真;否则,假的。
            var result = arr.Where(s => s.StartsWith("t"));
            Console.WriteLine(result.Count());
        }
    }


    #endregion

    #region 并发集合
    /*
     * 并发集合 ：由于常规的集合都是可以改变的，可以改变就意味这不是线程安全的
     * 所以这个时候 不变集合就站出来了
     * 
     * 在.net  的 System.Collections.Concurrent  中 有几个线程安全的集合类。线程安全的集合可以防止多个线程以冲突的方式访问集合
     * 
     * 为了对集合进行线程安全的访问，定义了一个 IProducerConsumerCollection<T> 接口（定义供制造者/使用者用来操作线程安全集合的方法）
     * 
     * ConcurrentQueue<T> 泛型集合类 
     * 1.定义了一种避免锁定的算法实现，使用再内部合并到一个链表中的32 项数组  （ConcurrentQueue 和 immutablequeqe 及其相似 但ConcurrentQueue 是线程安全的）
     * 2.实现 IProducerConsumerCollection 接口 提供了制造者和使用者来操作线程安全集合的方法
     * 3.TryAdd() 将一个对象添加到 IProducerConsumerCollection 中
     * 4.trytake() 从 IProducerConsumerCollection 中移除并返回一个对象
     * 
     * 
     * 
     * 还有  ConcurrentStack<T> , ConcurrentBag<T>,  ConcurrentDictionary<TKey,TValue>  ,
     * 
     * 
     * BlockingCollection<T> （在提取和添加元素之前，会阻塞线程并一直等待） 为实现 IProducerConsumerCollection<T> 的线程安全集合提供阻塞和限制功能。
     * 
     * 以上类都有 Concurrent 他们个作用其实和重名的普通集合是一样的，只不过他们是支持并发的（换句话说就是线程安全的）
     * 换句话说，就是 ConcurrentXXX 的集合都是线程安全的，如果某个动作不适用于线程的当前状态 就返回false  在继续之前都要确认添加和提取数据是否成功，不要相信集合会完成任务？
     * 
     */


    #region 创建管道

    /*
     * 创建啥管道？
     * 管道 就是程序执行的方式就像管道一样，但这个管道是由多个异步方法组成的，异步方法连接的节点就是刚刚看过的并发集合
     * 为啥要创建呢？
     * 作用是什么呢？
     * 优点呢？
     * 缺点呢？
     * 有没有替代方法呢？
     * 
     * 
     * 
     */
   /// <summary>
   /// 数据实体
   /// </summary>
    public class Info
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }

        public override string ToString() => $"{Count} times: {Word}";
    }
    /// <summary>
    /// 输出打印 改变颜色
    /// </summary>
    public class ColoredConsole
    {
        private static object syncOutput = new object();
        public static void WriteLine(string message)
        {
            //互斥锁 方法实行外后释放
            lock (syncOutput)
            {
                Console.WriteLine(message);
            }
        }
        public static void WriteLine(string message, string color)
        {
            lock (syncOutput)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(
                    typeof(ConsoleColor), color);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }

    /// <summary>
    /// 启动类
    /// </summary>
    class Programs_pipeline
    {
        public static void Programs_pipeline_Main()
        {
            StartPipelineAsync().Wait();
            ReadLine();
        }
   
        public static async Task StartPipelineAsync()
        {
            //创建并发集合存储线程返回数据
            var fileNames = new BlockingCollection<string>();
            
            var lines = new BlockingCollection<string>();
            var words = new ConcurrentDictionary<string, int>();
            var items = new BlockingCollection<Info>();
            var coloredItems = new BlockingCollection<Info>();

            //开启线程 执行异步方法
            Task t1 = PipelineStages.ReadFilenamesAsync(@"../..", fileNames);
            ColoredConsole.WriteLine("started stage 1");
            //开启线程执行异步反方法
            Task t2 = PipelineStages.LoadContentAsync(fileNames, lines);
            ColoredConsole.WriteLine("started stage 2");
            Task t3 = PipelineStages.ProcessContentAsync(lines, words);
            await Task.WhenAll(t1, t2, t3);
            ColoredConsole.WriteLine("stages 1, 2, 3 completed");
            Task t4 = PipelineStages.TransferContentAsync(words, items);
            Task t5 = PipelineStages.AddColorAsync(items, coloredItems);
            Task t6 = PipelineStages.ShowContentAsync(coloredItems);
            ColoredConsole.WriteLine("stages 4, 5, 6 started");

            await Task.WhenAll(t4, t5, t6);
            ColoredConsole.WriteLine("all stages finished");
        }

    }

    /// <summary>
    /// 管道节点
    /// </summary>
    public static class PipelineStages
    {

        /// <summary>
        /// 异步读取文件名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task ReadFilenamesAsync(string path, BlockingCollection<string> output)
        {
            return Task.Factory.StartNew(() =>
            {
                /*
                 * Directory 公开用于通过目录和子目录进行创建、移动和枚举的静态方法。
                 * EnumerateFiles 返回指定路径中与搜索模式匹配的文件名称的可枚举集合，还可以搜索子目录。
                 * 
                 * 
                 */
                foreach (string filename in Directory.EnumerateFiles(path, "*.cs",
                    SearchOption.AllDirectories))
                {
                    output.Add(filename);
                    ColoredConsole.WriteLine($"stage 1: added {filename}");
                }
                // CompleteAdding 实例标记为不再接受任何添加。

                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 异步读取文件名并加载内容，再写入下一队列中
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static async Task LoadContentAsync(BlockingCollection<string> input, BlockingCollection<string> output)
        {

            /*
             * 读取文件并将内容添加到另一个集合中
             * 该方法使用了输入集合传递的文件名，打开文件，并将文件内容输出到集合
             * 然后再迭代各项
             * 
             */
            //GetConsumingEnumerable 为集合中的项提供一个使用 IEnumerator<T>。
            //IEnumerator 好像是一个枚举接口（迭代器）
            foreach (var filename in input/*如果只使用input 只会迭代当前状态的集合*/.GetConsumingEnumerable())
            {
               
                //创建一个事务 打开指定文件
                using (FileStream stream = File.OpenRead(filename))
                {
                    //读取流 文件
                    var reader = new StreamReader(stream);
                    string line = null;
                    //从当前流中异步读取一行字符并将数据作为字符串返回。
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        output.Add(line);
                        ColoredConsole.WriteLine($"stage 2: added {line}");
                        await Task.Delay(20);
                    }
                }
            }
            output.CompleteAdding();
        }
        /// <summary>
        /// 获取输入集合行并拆分，并将每个词筛选到字典中
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, int> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(' ', ';', '\t', '{', '}', '(', ')',
                        ':', ',', '"');
                    foreach (var word in words.Where(w => !string.IsNullOrEmpty(w)))
                    {
                        //AddOrUpdate 如果该键不存在，则将键/值对添加到 ConcurrentDictionary<TKey, TValue> 中；
                        //如果该键已经存在，则更新 ConcurrentDictionary<TKey, TValue> 中的键/值对。
                        /*
                         * 如果键没有添加到字典中，第二个参数就定义应该设置的值，如果已存在于字典中，
                         * updateValueFactory 参数就定义值的改变方式，加一。
                         */


                        output.AddOrUpdate(key: word, addValue: 1, updateValueFactory: (s, i) => ++i);
                        ColoredConsole.WriteLine($"stage 3: added {word}");
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 从字典中获取数据转换成 info 类型并放到 BlockingCollection
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task TransferContentAsync(ConcurrentDictionary<string, int> input, BlockingCollection<Info> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var word in input.Keys)
                {
                    int value;
                    //TryGetValue 尝试从 System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> 获取与指定的键关联的值。

                    if (input.TryGetValue(word, out value))
                    {
                        var info = new Info { Word = word, Count = value };
                        output.Add(info);
                        ColoredConsole.WriteLine($"stage 4: added {info}");
                    }
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }

        public static Task AddColorAsync(BlockingCollection<Info> input, BlockingCollection<Info> output)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    if (item.Count > 40)
                    {
                        item.Color = "Red";
                    }
                    else if (item.Count > 20)
                    {
                        item.Color = "Yellow";
                    }
                    else
                    {
                        item.Color = "Green";
                    }
                    output.Add(item);
                    ColoredConsole.WriteLine($"stage 5: added color {item.Color} to {item}");
                }
                output.CompleteAdding();
            }, TaskCreationOptions.LongRunning);
        }
        public static Task ShowContentAsync(BlockingCollection<Info> input)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    ColoredConsole.WriteLine($"stage 6: {item}", item.Color);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }

    #endregion
    #endregion



    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {

            #region 管道 并发集合
           // Programs_pipeline.Programs_pipeline_Main();
            #endregion

            #region 不变集合类型和接口
            /*
             * 不变集合类型
             * 
             * 以不变集合为基础扩展了 集合存储方式类似于其它可变集合的 不变集合 例如 array list quque stack dictionary 等
             * 
             * immutablearray 一个数组类型的不变集合，
             * immutable
             * immutablelist   不变列表集合
             * immutablequeqe 不变队列
             * immutablestack 不变栈
             * immutabledictionary 不变字典
             * 
             * immutablesortedictionary 不变无序集合
             * immutablesortedset   不变有序集合
             * 
             * 
             */
            #endregion

            #region 不变的集合

            //Immutables.Immutabless();

            #endregion

            #region ObservableCollection

            // ObservableCollections.ObservableCollection_main();

            #endregion

            #region BitVector32

            //BitVector32s.BitVector32s_main();

            #endregion

            #region BitArray 点阵列

            // BitArrays.BitArrays_main();

            #endregion

            /*》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》》*/

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
            // Dictionarys.Dictionarys_main();
            #endregion

            #region lookup
            /*
            var racers = new List<Racer>();
            racers.Add(new Racer(26, "Jacques", "Villeneuve", "Canada", 11));
            racers.Add(new Racer(18, "Alan", "Jones", "Australia", 12));
            racers.Add(new Racer(11, "Jackie", "Stewart", "United Kingdom", 27));
            racers.Add(new Racer(15, "James", "Hunt", "United Kingdom", 10));
            racers.Add(new Racer(5, "Jack", "Brabham", "Australia", 14));

            var lookupRacers = racers.ToLookup(r => r.Country);

            foreach (Racer r in lookupRacers["Australia"])
            {
                WriteLine(r);
            }
            */
            #endregion

            #region 有序列表

            /*
             //用字符串创建一个新的字符串排序字典

             SortedDictionary<string, string> openWith =
                 new SortedDictionary<string, string>();

             // Add some elements to the dictionary. There are no 
             // duplicate keys, but some of the values are duplicates.
             //向字典中添加一些元素。没有
             //重复的键，但有些值是重复的。
             openWith.Add("txt", "notepad.exe");
             openWith.Add("bmp", "paint.exe");
             openWith.Add("dib", "paint.exe");
             openWith.Add("rtf", "wordpad.exe");

             // The Add method throws an exception if the new key is 
             // already in the dictionary.
             //如果新键是，添加方法会抛出一个异常
             //已经在字典里了
             try
             {
                 openWith.Add("txt", "winword.exe");
             }
             catch (ArgumentException)
             {
                 Console.WriteLine("已经存在一个Key = \"txt\"的元素。");
             }

             // The Item property is another name for the indexer, so you 
             // can omit its name when accessing elements. 
             // 项属性是索引器的另一个名称，因此您
             // 可以在访问元素时省略其名称。
             Console.WriteLine("键（） = \"rtf\", value = {0}.",
                 openWith["rtf"]);

             // The indexer can be used to change the value associated
             //索引器可用于更改关联的值
             // with a key.
             //用钥匙。
             openWith["rtf"] = "winword.exe";
             Console.WriteLine("For key = \"rtf\", value = {0}.",
                 openWith["rtf"]);

             // If a key does not exist, setting the indexer for that key
             // adds a new key/value pair.
             //如果键不存在，为该键设置索引器
             //添加一个新的键 / 值对。
             openWith["doc"] = "winword.exe";

             // The indexer throws an exception if the requested key is
             // not in the dictionary.
             //如果请求的键是，索引器抛出异常
             //字典里没有。
             try
             {
                 Console.WriteLine("For key = \"tif\", value = {0}.",
                     openWith["tif"]);
             }
             catch (KeyNotFoundException)
             {
                 Console.WriteLine("Key = \"tif\" is not found.");
             }

             // When a program often has to try keys that turn out not to
             // be in the dictionary, TryGetValue can be a more efficient 
             // way to retrieve values.
             //当一个程序经常不得不尝试钥匙，结果不是
             //在字典中，TryGetValue可能是一个更有效的
             //检索值的方法。
             string value = "";
             if (openWith.TryGetValue("tif", out value))
             {
                 Console.WriteLine("For key = \"tif\", value = {0}.", value);
             }
             else
             {
                 Console.WriteLine("Key = \"tif\" is not found.");
             }

             // ContainsKey can be used to test keys before inserting 
             // them.
             // ContainsKey可用于在插入之前测试密钥
             // 他们。
             if (!openWith.ContainsKey("ht"))
             {
                 openWith.Add("ht", "hypertrm.exe");
                 Console.WriteLine("Value added for key = \"ht\": {0}",
                     openWith["ht"]);
             }

             // When you use foreach to enumerate dictionary elements,
             // the elements are retrieved as KeyValuePair objects.
             //当使用foreach枚举字典元素时，
             //元素被检索为KeyValuePair对象。
             Console.WriteLine();
             foreach (KeyValuePair<string, string> kvp in openWith)
             {
                 Console.WriteLine("Key = {0}, Value = {1}",
                     kvp.Key, kvp.Value);
             }

             // To get the values alone, use the Values property.
             //要单独获取值，请使用values属性。
             SortedDictionary<string, string>.ValueCollection valueColl =
                 openWith.Values;

             // The elements of the ValueCollection are strongly typed
             // with the type that was specified for dictionary values.
             //值集合的元素是强类型的
             //使用为字典值指定的类型
             Console.WriteLine();
             foreach (string s in valueColl)
             {
                 Console.WriteLine("Value = {0}", s);
             }

             // To get the keys alone, use the Keys property.
             //要单独获取密钥，请使用keys属性。
             SortedDictionary<string, string>.KeyCollection keyColl =
                 openWith.Keys;

             // The elements of the KeyCollection are strongly typed
             // with the type that was specified for dictionary keys.
             //键集合的元素是强类型的
             //为字典键指定的类型。
             Console.WriteLine();
             foreach (string s in keyColl)
             {
                 Console.WriteLine("Key = {0}", s);
             }

             // Use the Remove method to remove a key/value pair.
             //使用Remove方法删除键/值对。
             Console.WriteLine("\nRemove(\"doc\")");
             openWith.Remove("doc");

             if (!openWith.ContainsKey("doc"))
             {
                 Console.WriteLine("Key \"doc\" is not found.");
             }

            */
            #endregion

            #region 集
            /*
                        //HashSet  不重复元素 的无序集合

                        var ct = new HashSet<string>(){"1","2","3" };

                        var tt = new HashSet<string>() { "1", "2" };

                        var pt = new HashSet<string>() { "one","two","three"};

                        if (pt.Add("4"))
                        {
                            Console.WriteLine("4 add");
                        }
                        if (!ct.Add("2"))
                        {
                            //元素不重复的集合。无序列表
                            Console.WriteLine("已存在数据");
                        }
                        //验证 tt 中 是否每个元素都包含在 ct
                        if (tt.IsSubsetOf(ct))
                        {
                            Console.WriteLine("tt 是 ct 自集");
                        }
                        //严重 tt 中是否有 ct 没有的额外元素
                        if (ct.IsSupersetOf(tt))
                        {
                            Console.WriteLine("ct 是 tt 超集");
                        }

                        tt.Add("haha");
                        if (pt.Overlaps(tt))
                        {
                            Console.WriteLine("两个集合间，是否有通用元素。（俗称交集）");
                        }

                        //返回不重复的元素的有序列表
                        var at = new SortedSet<string>(ct);
                        at.UnionWith(pt);
                        at.UnionWith(tt);
                        Console.WriteLine();

                        Console.WriteLine("输出参数");

                        foreach (var item in at)
                        {
                            Console.WriteLine(item);
                        }
            */

            #endregion



            ReadLine();
        }
    }
    #endregion













































































































    /**************   **********************/
}
