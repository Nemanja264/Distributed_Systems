import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class BioskopClientImpl extends UnicastRemoteObject implements BioskopClient {
    String name;
    BioskopClientImpl(String Name) throws RemoteException {
        super();
        this.name = Name;
    }
    @Override
    public void receiveMessage(String film, int sediste, BioskopClient bc) throws RemoteException {
        System.out.println(bc.getName() + " rezervisao je sediste " + sediste + " za film "+ film);
    }

    @Override
    public String getName() throws RemoteException {
        return this.name;
    }
}
