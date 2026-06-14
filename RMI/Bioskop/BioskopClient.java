import java.rmi.Remote;
import java.rmi.RemoteException;

public interface BioskopClient extends Remote {
    void receiveMessage(String film, int sediste, BioskopClient bc) throws RemoteException;
    String getName() throws RemoteException;
}
