using System.Threading.Tasks;

namespace ConsoleApplication
{
  public static class TaskExtensions
  {
    public static void Ignore(this Task task) { }
  }
}
