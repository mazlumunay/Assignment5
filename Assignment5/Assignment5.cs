class Mail
{
    public double Weight { get; }

    public Mail(double weight, bool isExpress, string destination)
    {
        Weight = weight;
        IsExpress = isExpress;
        Destination = destination;
    }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Destination);
    }

    public bool IsExpress { get; }
    public string Destination { get; }

    public virtual double CalculatePostage()
    {
        return 0.0; // Default implementation
    }
}

class Lettre : Mail
{
    public string Format { get; }

    public Lettre(double weight, bool isExpress, string destination, string format)
        : base(weight, isExpress, destination)
    {
        Format = format;
    }

    public override double CalculatePostage()
    {
        double baseFare = Format == "A4" ? 2.50 : 3.50;
        double postage = IsExpress ? 2 * (baseFare + 1.0 * Weight / 1000) : baseFare + 1.0 * Weight / 1000;
        return postage;
    }
}

class Advertisement : Mail
{
    public Advertisement(double weight, bool isExpress, string destination)
        : base(weight, isExpress, destination)
    {
    }

    public override double CalculatePostage()
    {
        double postage = IsExpress ? 2 * (5.0 * Weight / 1000) : 5.0 * Weight / 1000;
        return postage;
    }
}

class Parcel : Mail
{
    public double Volume { get; }

    public Parcel(double weight, bool isExpress, string destination, double volume)
        : base(weight, isExpress, destination)
    {
        Volume = volume;
    }

    public override double CalculatePostage()
    {
        if (Volume > 50 || !IsValid())
            return 0.0;

        double postage = IsExpress ? 2 * (0.25 * Volume + Weight / 1000) : 0.25 * Volume + Weight / 1000;
        return postage;
    }

    public bool IsValidParcel()
    {
        return IsValid() && Volume <= 50;
    }
}

class Box
{
    private List<Mail> mails;
    private int maxSize;

    public Box(int maxSize)
    {
        this.maxSize = maxSize;
        mails = new List<Mail>();
    }

    public void addMail(Mail mail)
    {
        if (mails.Count < maxSize)
            mails.Add(mail);
    }

    public double stamp()
    {
        double totalPostage = 0.0;
        foreach (var mail in mails)
        {
            totalPostage += mail.CalculatePostage();
        }
        return totalPostage;
    }

    public int mailIsInvalid()
    {
        int count = 0;
        foreach (var mail in mails)
        {
            if (!mail.IsValid())
                count++;
            else if (mail is Parcel && !((Parcel)mail).IsValidParcel())
                count++;
        }
        return count;
    }

    public void display()
    {
        double totalPostage = 0.0;
        foreach (var mail in mails)
        {
            string type = mail.GetType().Name;
            Console.WriteLine($"{type}");
            if (!mail.IsValid())
            {
                Console.WriteLine("(Invalid courier)");
                Console.WriteLine($"Weight: {mail.Weight} grams");
                Console.WriteLine($"Express: {(mail.IsExpress ? "yes" : "no")}");
                Console.WriteLine($"Destination: {mail.Destination}");
                Console.WriteLine($"Price: $0.0");
                if (mail is Lettre)
                    Console.WriteLine($"Format: {(mail as Lettre).Format}");
                else if (mail is Parcel)
                    Console.WriteLine($"Volume: {(mail as Parcel).Volume} liters");
                Console.WriteLine();
            }
            else
            {
                double postage = mail.CalculatePostage();
                Console.WriteLine($"Weight: {mail.Weight} grams");
                Console.WriteLine($"Express: {(mail.IsExpress ? "yes" : "no")}");
                Console.WriteLine($"Destination: {mail.Destination}");
                Console.WriteLine($"Price: ${postage.ToString("0.0")}");
                if (mail is Lettre)
                    Console.WriteLine($"Format: {(mail as Lettre).Format}");
                else if (mail is Parcel)
                    Console.WriteLine($"Volume: {(mail as Parcel).Volume} liters");
                Console.WriteLine();
                totalPostage += postage;
            }
        }
        Console.WriteLine($"The total amount of postage is {totalPostage.ToString("0.0")}");
        
    }
}
