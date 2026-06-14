import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class ClientMain {
    public static void main(String[] args) throws Exception {
        Registry r = LocateRegistry.getRegistry();
        ChatServer cs = (ChatServer) r.lookup("ChatServer");

        ChatClient ana = new ChatClientImpl("Ana");
        ChatClient bob = new ChatClientImpl("Bob");

        cs.pridruziSe("opsta", ana);
        cs.pridruziSe("opsta", bob);

        cs.posaljiPoruku("opsta", "Zdravo svima!", ana);   // Bob prima, Ana ne
        cs.posaljiPoruku("opsta", "Cao Ana!", bob);        // Ana prima, Bob ne

        Thread.sleep(Long.MAX_VALUE);
    }
}
