using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

   namespace 数据结构和算法
{
    class Prgram
    {


        /* 
         * 数据结构的基本概念：
         * 
         * 数据：什么数据呢，可以类比成对象，世间万物结尾对象，同样的数据也是一样的  （单个数据）
         * 数据元素：就是一类数据的集合，可以说是一条数据元素，一条数据元素里面有n个数据  （一条数据）
         * 数据项：数据元素的集合                                                         （数据表）
         * 
         * 
         * 数据的逻辑结构：总的来说可以分为 线性结构（线性结构）和非线性结构（树结构和图结构）
         * 
         * 线性结构：除第一个和最后一个数据元素外，每个数据元素只有一个前驱和一个后继数据元素。每个数据都是紧密连接的，（结绳即时）
         * 树结构：上下级一对多的关系，出去第一个根节点外，每个数据只有一个前驱，却可以有好多好多个后继元素（类似于族谱，开枝散叶）
         * 图结构： 多对多的关系，每个节点可以有若干个前去或者后继元素，（人与人之间都是平等的，类似于人与人之间的相识网）
         * 
         * 
         * 数据的储存结构：
         * 
         * 顺序储存结构： 将数据元素储存到一块连续地址空间的内存中（感觉类似于弹匣，压栈和出栈），逻辑和物理地址都相邻， （就是说你是人类（逻辑地址），在地球（物理地址））
         * 指针：指向物理储存单元地址的变量，由数据元素域（data）和指针域（ntext）构成一个结构体的节点。链式列表的储存方式
         * 链式储存结构：用指针将将逻辑上相邻（注意物理地址不一定相邻嗷）相的节点相互关联
         * 
         * 数据的操作：数据的操作是讨论某种数据类型数据应具备的操作的逻辑功能，抽象角度下的操作一般和数据的逻辑结构一起讨论
         * 疑问：数据的逻辑结构是对数据的物理地址的抽象吗？
         * 
         */








        /*
         * 时间复杂度使用于 对各个条件都相同（代码除外）的条件下，比较不同结构的算法的差异的东西
         *
         * 时间复杂度是一个函数，它定性描述了该算法的运行时间。这是一个关于代表算法输入值的字符串的长度的函数。
         * 时间复杂度常用大O符号表述，不包括这个函数的低阶项和首项系数。 [1] 
         * 
         * 时间复杂度: T(n) = O(f(n))
         * 从表达式可以看出，一个算法的运行时间(T(n))是 他的运算的层级（fn） 成正比的，
         * 从表达式可以看出，当他的运算时间 和他的运算层级的比 可以看成是一个常数
         * 
         * 数据的三种结构
         * 
         * 线性结构：（结绳记事大法） ，每一个数据都紧密连接，有规律的，他是一对一的关系，有唯一前后元素
         * 树状结构：（族谱），父与子的关系
         * 图形结构：（同类之间的关系），人与人之间的关系网。
         * 
         * 
         * 
         * 
         * 链式： 以指针（啥是指针呢？相当于指南针，指定方向用的，指定啥方向呢？下一个节点的（data部分那，并给下一部分的data 赋值））
         * 的方式连接，没有规律的，反指针方向的值 就是指针所指的值。
         * 
         */


    }
}






















































































































































































































































































































































































































































































































































































































































































/// <summary>
/// 快速排序打法
/// 
/// </summary>
namespace test
{
    class QuickSort
    {
        static void Main(string[] args)
        {
            int[] array = { 49, 38, 65, 97, 76, 13, 27 };
            sort(array, 0, array.Length - 1);
            Console.ReadLine();
        }
        /**一次排序单元，完成此方法，key左边都比key小，key右边都比key大。


**@param array排序数组 


**@param low排序起始位置 


**@param high排序结束位置


**@return单元排序后的数组 */
        private static int sortUnit(int[] array, int low, int high)
        {
            int key = array[low];
            while (low < high)
            {
                /*从后向前搜索比key小的值*/
                while (array[high] >= key && high > low)
                    --high;
                /*比key小的放左边*/
                array[low] = array[high];
                /*从前向后搜索比key大的值，比key大的放右边*/
                while (array[low] <= key && high > low)
                    ++low;
                /*比key大的放右边*/
                array[high] = array[low];
            }
            /*左边都比key小，右边都比key大。//将key放在游标当前位置。//此时low等于high */
            array[low] = key;
            foreach (int i in array)
            {
                Console.Write("{0}\t", i);
            }
            Console.WriteLine();
            return high;
        }
        /**快速排序 
*@paramarry 
*@return */
        public static void sort(int[] array, int low, int high)
        {
            if (low >= high)
                return;
            /*完成一次单元排序*/
            int index = sortUnit(array, low, high);
            /*对左边单元进行排序*/
            sort(array, low, index - 1);
            /*对右边单元进行排序*/
            sort(array, index + 1, high);
        }
    }
}








