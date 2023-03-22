using System;

public partial class Utility{
    public static int ToInt(string criterio)
    {
        if(!string.IsNullOrEmpty(criterio) || !string.IsNullOrWhiteSpace(criterio))
        {
            string numero = "";
            for(int i = 0; i<criterio.Length;i++)
            {
                if (char.IsNumber(criterio[i]))
                {
                    numero += criterio[i];
                }
            }
            if(!string.IsNullOrEmpty(numero) || !string.IsNullOrWhiteSpace(numero))
            {
                return Convert.ToInt32(numero);
            }else{
                return -1;
            }
        }else{
            return -1;
        }
    }
}