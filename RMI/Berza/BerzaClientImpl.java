import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class BerzaClientImpl extends UnicastRemoteObject implements BerzaClient {
    String name;
    BerzaClientImpl(String Name) throws RemoteException {super(); this.name=Name;}

    @Override
    public void receiveMessage(String akcija, Double staraCena, Double novaCena) throws RemoteException {
        System.out.println(akcija + " Stara cena:"+staraCena+" Nova cena:"+novaCena);
    }

    @Override
    public String getName() throws RemoteException {
        return this.name;
    }
}
