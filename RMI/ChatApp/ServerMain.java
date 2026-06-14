import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class ServerMain {
    public static void main(String[] args) throws Exception {
        Registry r = LocateRegistry.createRegistry(1099);
        r.rebind("ChatServer", new ChatServerImpl());

        System.out.println("Server is running");
    }
}
