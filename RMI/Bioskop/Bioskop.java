import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Bioskop extends Remote {
    void dodajProjekciju(String film, int brSedista) throws RemoteException;
    void rezervisiSediste(String film, int sediste, BioskopClient client) throws RemoteException;
    void pratiProjekciju(String film, BioskopClient client) throws RemoteException;
}
