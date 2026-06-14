import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class Client {
    public static void main(String[] args) throws Exception {
        Registry r = LocateRegistry.getRegistry();
        Bioskop b = (Bioskop) r.lookup("Bioskop");

        BioskopClient k1 = new BioskopClientImpl("Pera");
        BioskopClient k2 = new BioskopClientImpl("Mika");
        BioskopClient k3 = new BioskopClientImpl("Mario");

        b.dodajProjekciju("Matrix", 50);
        b.pratiProjekciju("Matrix", k1);
        b.pratiProjekciju("Matrix", k2);

        b.rezervisiSediste("Matrix", 5, k1);
        b.rezervisiSediste("Matrix", 10, k2);
        b.rezervisiSediste("Matrix", 11, k3);

    }
}