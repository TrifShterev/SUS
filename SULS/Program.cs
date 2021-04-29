using System;
using System.Threading.Tasks;
using SUS.MvcFramework;

namespace SULS
{
  public  class Program
    {
      public  static async Task Main(string[] args)
      {
          await Host.CreateHostAsync(new StartUp());
      }
    }
}
