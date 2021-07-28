using System.IO;
using System.Threading.Tasks;

namespace Oasis.Aplicacao.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToCharArrayAsync(this Stream @this) 
        {
            var byteArray = new byte[@this.Length];
            using(@this) 
            {
                await @this.ReadAsync(byteArray, 0, byteArray.Length);
            }

            return byteArray;
        }
        public static byte[] ToCharArray(this Stream @this) 
        {
            var byteArray = new byte[@this.Length];
            using(@this) 
            {
                @this.Read(byteArray, 0, byteArray.Length);
            }

            return byteArray;
        }
    }
}