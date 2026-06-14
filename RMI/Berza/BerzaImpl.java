import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class BerzaImpl extends UnicastRemoteObject implements Berza {
    private Map<String, Double> akcije = new HashMap<>();
    private Map<String, List<BerzaClient>> subs = new HashMap<>();
    BerzaImpl() throws RemoteException {super();}
    public void createAkcija(String akcija)
    {
        akcije.put(akcija, new Double(0));
        subs.put(akcija, new ArrayList<>());
    }

    @Override
    public void azurirajCenu(String akcija, Double novaCena) throws RemoteException {
        if(!akcije.containsKey(akcija)) createAkcija(akcija);

        Double staraCena = akcije.get(akcija);
        akcije.put(akcija, novaCena);
        for(BerzaClient bc: subs.get(akcija))
        {
            try {
                bc.receiveMessage(akcija,staraCena,novaCena);
            }
            catch (RemoteException re)
            {
                return;
            }
        }
    }

    @Override
    public void pretplatiSe(String akcija, BerzaClient bc) throws RemoteException {
        if(!akcije.containsKey(akcija)) createAkcija(akcija);

        subs.get(akcija).add(bc);
    }

    @Override
    public void otkaziPretplatu(String akcija, BerzaClient bc) throws RemoteException {
        if(akcije.containsKey(akcija)) {
            subs.get(akcija).removeIf(c -> {
                try {
                    return c.getName().equals(bc.getName());
                }
                catch (RemoteException re)
                {
                    return true;
                }
            });
        }
    }
}
