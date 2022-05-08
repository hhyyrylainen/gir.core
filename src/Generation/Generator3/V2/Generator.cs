namespace Generator3.V2.Controller;

public interface Generator<T>
{
    CodeUnit Generate(T obj);
}
