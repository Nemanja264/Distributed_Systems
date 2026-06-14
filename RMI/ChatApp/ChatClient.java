import java.rmi.Remote;
import java.rmi.RemoteException;

public interface ChatClient extends Remote {
    void receiveMessage(String room, String message, ChatClient publisher) throws RemoteException;
    String getIme() throws RemoteException;
}
