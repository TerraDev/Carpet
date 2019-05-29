static List<string[,]> Purchasing_power(List<string[,]> matrix,int price)
        {
            List<string[,]> output = new List<string[,]>();
            Dictionary<string[,], int> value_carpet = new Dictionary<string[,], int>();
            Random rnd = new Random();
            foreach (var item in matrix)
            {
                value_carpet.Add(item, rnd.Next(1, 20));
            }
            var items = from pair in value_carpet
                        orderby pair.Value ascending
                        select pair;
            foreach (var item in items)
            {
                if (price > item.Value)
                {
                    output.Add(item.Key);
                    price -= item.Value;
                } 
            }

            return output;
        }
