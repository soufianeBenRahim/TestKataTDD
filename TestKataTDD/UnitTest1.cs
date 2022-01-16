using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestKataTDD
{
    [TestClass]
    public class UnitTest1
    {
        public int Add(string numbers)
        {

            if (numbers == "")
            {
                return 0;
            }
            var errorMessage = "";
            var delemiter = ",";
            var Newnumbers = numbers;
            if (numbers.StartsWith("//"))
            {
                if (numbers.StartsWith("//["))
                {
                    var index = numbers.IndexOf("]\n");
                    delemiter = Newnumbers.Substring(3, index-3);
                    var listeOfDelemetres = delemiter.Split("][");
                    Newnumbers = numbers.Substring(index+2);
                    foreach (var dilimitre in listeOfDelemetres)
                    {
                        Newnumbers=Newnumbers.Replace(dilimitre, ",");
                    }
                    delemiter = ",";
                }
                else
                {
                    delemiter = Newnumbers.Substring(2, 1);
                    Newnumbers = numbers.Substring(4);
                }
            }
            var spliter = Newnumbers.Split(delemiter);
            int sum= spliter.Sum(x => 
            {
                var nestedSpliter = x.Split("\n");
                int sum = 0;
                foreach (var item in nestedSpliter)
                {
                    var number = Convert.ToInt32(item);
                    if (number < 0)
                    {
                        if (errorMessage.Equals(""))
                        {
                            errorMessage += $"the number {item} is nigative!";
                        }
                        else
                        {
                            errorMessage += $"\n the number {item} is nigative!";
                        }
                        
                    }
                    else if(number<=1000)
                    {
                        sum += number;
                    }

                }
                return sum;
            });
            if (!errorMessage.Equals(""))
            {
                throw new Exception(errorMessage);
            }
            else
            {
                return sum;
            }
        }
        [TestMethod]
        public void should_return_0_when_input_is_Empty_string()
        {
            Assert.AreEqual(Add(""),0);
        }
        [TestMethod]
        public void should_return_1_when_input_is_1()
        {
            Assert.AreEqual(Add("1"), 1);
        }
        [TestMethod]
        public void should_return_3_when_input_is_1_coma_2()
        {
            Assert.AreEqual(Add("1,2"),3);
        }
        [TestMethod]
        public void should_calculate_the_new_line_as_a_siparator()
        {
            Assert.AreEqual(Add("1\n3,2"), 6);
        }
        [TestMethod]
        public void should_shange_the_dfault_delemiter_in_a_separate_ligne_in_the_biginner()
        {
            Assert.AreEqual(Add("//;\n3;2"), 5);
        }
        [TestMethod]
        public void should_Return_exception_messsage_when_they_are_a_negative_number()
        {
            var exeception =Assert.ThrowsException<Exception>(()=>Add("3,-2,-3"));
            Assert.AreEqual(exeception.Message, "the number -2 is nigative!\n the number -3 is nigative!");
        }
        [TestMethod]
        public void number_beger_than_1000_should_be_ignored()
        {
            Assert.AreEqual(Add("2,1001"), 2);
        }
        [TestMethod]
        public void should_accept_the_delemiter_with_multiple_carater()
        {
            Assert.AreEqual(Add("//[***]\n1***2***3"), 6);
        }
        [TestMethod]
        public void should_accept_the_multiple_delemiter_with_singel_carater()
        {
            Assert.AreEqual(Add("//[*][%]\n1*2%3"), 6);
        }
        [TestMethod]
        public void should_accept_the_multiple_delemiter_with_multiple_carater()
        {
            Assert.AreEqual(Add("//[**][%%]\n1**2%%3"), 6);
        }
        [TestMethod]
        public void should_accept_the_multiple_delemiter_with_multiple_carater_2()
        {
            Assert.AreEqual(Add("//[*.][%*]\n1*.2%*3"), 6);
        }
        [TestMethod]
        public void should_accept_the_multiple_delemiter_with_multiple_carater_3()
        {
            Assert.AreEqual(Add("//[*.][%*]\n11*.12%*93"), 116);
        }
    }
}
