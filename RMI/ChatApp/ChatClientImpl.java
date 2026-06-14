import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class ChatClientImpl extends UnicastRemoteObject implements ChatClient {
    private String name;

    ChatClientImpl(String Name) throws RemoteException {
        super();
        this.name = Name;
    }
    @Override
    public void receiveMessage(String room, String message, ChatClient publisher) throws RemoteException {
        System.out.println("Room:" + room + " Message:"+message+" From:"+publisher.getIme());
    }

    @Override
    public String getIme() {
        return this.name;
    }
}
