namespace Post;

class Assignment5
{
    public static void Main(string[] args)
    {
        //creation of a mailbox 
        // the maximum size of a box is 30
        Box box = new Box(30);

        Lettre lettre1 = new Lettre(200, true, "Chemin des Acacias 28, 1009 Pully", "A3");
        Lettre lettre2 = new Lettre(800, false, "", "A4"); // invalid

        Advertisement adv1 = new Advertisement(1500, true, "Les Moilles  13A, 1913 Saillon");
        Advertisement adv2 = new Advertisement(3000, false, ""); // invalid

        Parcel parcel1 = new Parcel(5000, true, "Grand rue 18, 1950 Sion", 30);
        Parcel parcel2 = new Parcel(3000, true, "Chemin des fleurs 48, 2800 Delemont", 70); // invalid parcel
		
        box.addMail(lettre1);
        box.addMail(lettre2);
        box.addMail(adv1);
        box.addMail(adv2);
        box.addMail(parcel1);
        box.addMail(parcel2);	
		

		Console.WriteLine("The total amount of stamp is " +
		                  box.stamp());
		box.display();
		
        Console.WriteLine("The box contains " + box.mailIsInvalid()
                                               + " invalid mails");
    }
}