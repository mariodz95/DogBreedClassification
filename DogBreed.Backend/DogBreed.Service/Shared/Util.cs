using Microsoft.AspNetCore.Http;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DogBreed.Shared
{
    public class Util
    {
        public byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }


        public static Dictionary<string, float> GetSlotNames(DataViewSchema schema, string name, float[] scores)
        {
            Dictionary<string, float> result = new Dictionary<string, float>();


            var column = schema.GetColumnOrNull(name);

            var slotNames = new VBuffer<ReadOnlyMemory<char>>();
            column.Value.GetSlotNames(ref slotNames);
            var names = new string[slotNames.Length];
            var num = 0;
            foreach (var denseValue in slotNames.DenseValues())
            {
                result.Add(denseValue.ToString(), scores[num++]);
            }

            return result.OrderByDescending(c => c.Value).ToDictionary(i => i.Key, i => i.Value);
        }
    }
}
