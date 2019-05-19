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
public static string getMinimumPenalty(string x, string y, int pxy, int pgap)  
    {  
        int i, j; // intialising variables  
            
        int m = x.Length; // length of gene1  
        int n = y.Length; // length of gene2  
            
        // table for storing optimal substructure answers  
        int[,] dp = new int[n+m+1,n+m+1]; 
        for(int q = 0; q < n+m+1; q++) 
            for(int w = 0; w < n+m+1; w++) 
                dp[q,w] = 0; 
        
        // intialising the table   
        for (i = 0; i <= (n+m); i++)  
        {  
            dp[i,0] = i * pgap;  
            dp[0,i] = i * pgap;  
        }      
        
        // calcuting the minimum penalty  
        for (i = 1; i <= m; i++)  
        {  
            for (j = 1; j <= n; j++)  
            {  
                if (x[i - 1] == y[j - 1])  
                {  
                    dp[i,j] = dp[i - 1,j - 1];  
                }  
                else
                {  
                    dp[i,j] = Math.Min(Math.Min(dp[i - 1,j - 1] + pxy ,   
                                    dp[i - 1,j] + pgap)    ,   
                                    dp[i,j - 1] + pgap    );  
                }  
            }  
        }  
        
        // Reconstructing the solution  
        int l = n + m; // maximum possible length  
            
        i = m; j = n;  
            
        int xpos = l;  
        int ypos = l;  
        
        // Final answers for the respective strings  
        int[] xans = new int[l+1]; 
        int [] yans = new int[l+1];  
            
        while ( !(i == 0 || j == 0))  
        {  
            if (x[i - 1] == y[j - 1])  
            {  
                xans[xpos--] = (int)x[i - 1];  
                yans[ypos--] = (int)y[j - 1];  
                i--; j--;  
            }  
            else if (dp[i - 1,j - 1] + pxy == dp[i,j])  
            {  
                xans[xpos--] = (int)x[i - 1];  
                yans[ypos--] = (int)y[j - 1];  
                i--; j--;  
            }  
            else if (dp[i - 1,j] + pgap == dp[i,j])  
            {  
                xans[xpos--] = (int)x[i - 1];  
                yans[ypos--] = (int)'_';  
                i--;  
            }  
            else if (dp[i,j - 1] + pgap == dp[i,j])  
            {  
                xans[xpos--] = (int)'_';  
                yans[ypos--] = (int)y[j - 1];  
                j--;  
            }  
        }  
        while (xpos > 0)  
        {  
            if (i > 0) xans[xpos--] = (int)x[--i];  
            else xans[xpos--] = (int)'_';  
        }  
        while (ypos > 0)  
        {  
            if (j > 0) yans[ypos--] = (int)y[--j];  
            else yans[ypos--] = (int)'_';  
        }  
        
        // Since we have assumed the answer to be n+m long,   
        // we need to remove the extra gaps in the starting   
        // id represents the index from which the arrays  
        // xans, yans are useful  
        int id = 1;  
        for (i = l; i >= 1; i--)  
        {  
            if ((char)yans[i] == '_' && (char)xans[i] == '_')  
            {  
                id = i + 1;  
                break;  
            }  
        }  
        return yans;
