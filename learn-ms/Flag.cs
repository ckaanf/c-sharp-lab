using System;
public class Flag
{
  public static void flagSubscription()
  {
    Random random = new Random();
    int daysUntilExpiration = random.Next(12);
    int discountPercentage = 0;

    // Your code goes here
    if (daysUntilExpiration <= 10)
    {
      Console.WriteLine($"Your subscription expire soon. Renew now!");
    }
    else if (daysUntilExpiration <= 5)
    {
      discountPercentage = 10;
      Console.WriteLine($"Your subscription expires in {daysUntilExpiration} days.");
      Console.WriteLine($"Renew now and save {discountPercentage}%!");
    }
    else if (daysUntilExpiration <= 1)
    {
      discountPercentage = 20;
      Console.WriteLine($"Your subscription expires in {daysUntilExpiration} days.");
      Console.WriteLine($"Renew now and save {discountPercentage}%!");
    }
    else if (daysUntilExpiration == 0)
    {
      Console.WriteLine("Your subscription has expired.");
    }
    else
    {

    }
  }
}