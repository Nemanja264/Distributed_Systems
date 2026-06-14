import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class Server {
    public static void main(String[] args) throws Exception {
        Registry r = LocateRegistry.createRegistry(1099);
        r.rebind("Bioskop", new BioskopImpl());
        System.out.println("Bioskop server running");
    }
}
