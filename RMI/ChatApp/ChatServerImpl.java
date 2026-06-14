import java.rmi.Remote;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ChatServerImpl extends UnicastRemoteObject implements ChatServer {
    private Map<String, List<ChatClient>> rooms = new HashMap<>();

    ChatServerImpl() throws RemoteException {super();}

    public void createRoom(String roomName)
    {
        rooms.put(roomName, new ArrayList<>());
    }
    @Override
    public synchronized void pridruziSe(String soba, ChatClient user) throws RemoteException {
        if(!rooms.containsKey(soba)) createRoom(soba);

        rooms.get(soba).add(user);
        System.out.println("Sobi "+soba+" se pridruzio "+user.getIme());
    }

    @Override
    public synchronized void posaljiPoruku(String soba, String message, ChatClient publisher) throws RemoteException {
        if(!rooms.containsKey(soba)) return;

        for(ChatClient c: rooms.get(soba))
        {
            if (!c.getIme().equals(publisher.getIme()))
            {
                try{
                    c.receiveMessage(soba,message,publisher);
                }
                catch (RemoteException re)
                {
                    return;
                }
            }
        }
    }

    @Override
    public synchronized void napustiSobu(String soba, ChatClient user) throws RemoteException {
        if(rooms.containsKey(soba)) {
            rooms.get(soba).removeIf(u ->{
                try{
                    return u.getIme().equals(user.getIme());
                }
                catch (RemoteException re)
                {
                    return true;
                }
            });
        }
    }
}
