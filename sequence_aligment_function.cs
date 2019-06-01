public static string matrix_to_array(string[,] matrix)
        {
            string array = "", temp;
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 400; j++)
                {
                    temp = array;
                    array = temp.Insert(temp.Length, matrix[i,j]);
                }
            }
            return array;
        }
static int sequence_aligment(string[] array1, string[] array2)
        {
            int point = 0;
            for (int i = 0; i < 120000; i++)
            {
                if (array1[i] == array2[i])
                    point++;
            }
            return point;
        }
