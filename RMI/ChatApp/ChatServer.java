import java.rmi.Remote;
import java.rmi.RemoteException;

public interface ChatServer extends Remote {
    void pridruziSe(String soba, ChatClient user) throws RemoteException;
    void posaljiPoruku(String soba, String message, ChatClient publisher) throws RemoteException;
    void napustiSobu(String soba, ChatClient user) throws RemoteException;
}
