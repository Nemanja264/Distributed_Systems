import java.rmi.Remote;
import java.rmi.RemoteException;

public interface BerzaClient extends Remote {
    void receiveMessage(String akcija, Double staraCena, Double novaCena) throws RemoteException;
    String getName() throws RemoteException;
}
