namespace PaymentApp.Model
{
    public class NestedClassForTest
    {
    }
    public class A
    {
        public int AId { get; set; }
        public B MyProperty { get; set; }
    }



    public class B
    {
        public int BId { get; set; }

        public C MyProperty { get; set; }

    }

    public class C
    {
        public int CId { get; set; }

        public D MyProperty { get; set; }

    }

    public class D
    {
        public int DId { get; set; }

        public E MyProperty { get; set; }

    }

    public class E
    {
        public int EId { get; set; }

        public F MyProperty { get; set; }

    }

    public class F
    {
    }

    public class G
    {
    }

    public class H
    {
    }
}
