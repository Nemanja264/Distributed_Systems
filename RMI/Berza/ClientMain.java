import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class ClientMain {
    public static void main(String[] args) throws Exception
    {
        Registry r = LocateRegistry.getRegistry();
        Berza b = (Berza) r.lookup("Berza");

        BerzaClient k1 = new BerzaClientImpl("Ana");
        BerzaClient k2 = new BerzaClientImpl("Marko");

        b.azurirajCenu("AAPL", 150.0);    // implicitno kreira, nema pretplatnika
        b.pretplatiSe("AAPL", k1);
        b.pretplatiSe("AAPL", k2);

        b.azurirajCenu("AAPL", 155.5);    // oba klijenta dobijaju obaveštenje
        b.azurirajCenu("AAPL", 153.0);

        b.otkaziPretplatu("AAPL", k1);
        b.azurirajCenu("AAPL", 160.0);
    }
}
