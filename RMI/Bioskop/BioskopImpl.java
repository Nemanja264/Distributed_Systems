import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class BioskopImpl extends UnicastRemoteObject implements Bioskop {
    private Map<String, boolean[]> projekcija = new HashMap<>();
    private Map<String, List<BioskopClient>> pratioci = new HashMap<>();
    BioskopImpl() throws RemoteException{super();}
    @Override
    public synchronized void dodajProjekciju(String film, int brSedista) throws RemoteException {
        if(!projekcija.containsKey(film))
        {
            projekcija.put(film, new boolean[brSedista]);
            pratioci.put(film, new ArrayList<>());
        }
    }

    @Override
    public synchronized void rezervisiSediste(String film, int sediste, BioskopClient client) throws RemoteException {
        if(!projekcija.containsKey(film)) throw new RemoteException("Projekcija " + film + " ne postoji");

        boolean[] sedista = projekcija.get(film);
        if(sediste < 0 || sediste >= sedista.length)
            throw  new RemoteException("Unesi drugi broj sediste ovo nije u opsegu");

        if(sedista[sediste])
            throw  new RemoteException("Sediste " + sediste + " je zauzeto");

        sedista[sediste] = true;
        for(BioskopClient bc: pratioci.get(film))
        {
            try {
                bc.receiveMessage(film,sediste,client);
            }
            catch (RemoteException re)
            {
                return;
            }
        }
    }

    @Override
    public synchronized void pratiProjekciju(String film, BioskopClient client) throws RemoteException {
        if(!projekcija.containsKey(film)) throw new RemoteException("Projekcija " + film + " ne postoji");

        pratioci.get(film).add(client);
        System.out.println(client.getName() + " prati obavestenja za " + film);
    }
}
