import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Berza extends Remote {
    void azurirajCenu(String akcija, Double novaCena) throws RemoteException;
    void pretplatiSe(String akcija, BerzaClient bc) throws RemoteException;
    void otkaziPretplatu(String akcija, BerzaClient bc) throws RemoteException;
}
