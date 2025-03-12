namespace Nuova_cartella;
using System;
using System.Collections;
using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Lettura.Host();
        Lettura.IpRete0();
        int app=Memorizza.ContaHost(0);
        while(app>0)
        {
            Calcolo.CalcoloBroadcast();
            Calcolo.CalcoloRete();
            Calcolo.CalcoloSubnet();
            app--;
        }
        Impaginatura.tabella();
        Gatto.StampaGatto();
    }
}

class Lettura
{
    public static void Host()
    {
    int host=1;
    int indhost=0;
    int indbit=0;
        Console.WriteLine("Scrivi tutti i numeri di host ");
        while(host!=0)
            {
                Console.Write("->");
                host=Error.CondizioniHost();
                if(host >=2)
                {
                    Memorizza.MemorizzaHost(indhost, host);//passo posizione e valore da memorizzare
                    Memorizza.ContaHost(1);
                    indhost++;
                    Calcolo.CalcoloBit(indbit,host);//passo il valore di host per calcolare i bit
                    indbit++;
                }
            }
    }

    public static void IpRete0()
    {
        int riga=0;
        Console.WriteLine("Scrivi la prima rete suddivisa in 4 ");
        for(int colonna=0; colonna<4; colonna++)
        {
            int app;
            Console.Write("->");
            app=Convert.ToInt32(Console.ReadLine());
            Memorizza.MemorizzaRete(riga,colonna,app);//passaggio dati per memorizzare la prima rete da input
        }
        riga++;
    }
}
class Calcolo()
{
    public static void CalcoloBit(int posizione,int host)
    {
        int bit;
        for(bit=0; host>(Math.Pow(2,bit)-2); bit++);//trova i bit host
        bit=32-bit;//trovati i bit rete
        Memorizza.MemorizzaBit(posizione,bit);//passo posizione e valore del bit da memorizzare
    }
    static int posizione=0;
    public static void CalcoloMagicNumber(int bit)
    {
        int ValoreAggiunto;
        int app;
        app=bit%8;//divisione della barra (es./20) per trovare i bit rete (es.4)
        app=8-app;//sottrazione bit rete da 8 per trovare i bit host

        ValoreAggiunto=Convert.ToInt32(Math.Pow(2,app)-1);//valore da aggiungere alla rete per trovare il broadcast
        Memorizza.MemorizzaValoreAggiunto(posizione,ValoreAggiunto);//passo posizione e valore da memorizzare
        ValoreAggiunto=0;
    }

    public static void CalcolOtteti(int bit)
    {
        while(bit>8)//trovo su che otteto lavorare
        {
            bit=bit/8;
        }
        Memorizza.MemorizzaOtteti(posizione,bit);//memorizzo gli otteti
        posizione++;
    }

    static int colonnaBroadcast=0,riga=0;
    public static void CalcoloBroadcast()
    {
        int app=Memorizza.RestituisciOtteti(riga);//variabile di appoggio per gli otteti
        while(app>0)//app-1 trovo la parte di rete da ricopiare
        {
        Memorizza.MemorizzaBroadcast(riga, colonnaBroadcast, Memorizza.RestituisciRete(riga, colonnaBroadcast));//copia la parte di rete che non cambia
        app--;
        colonnaBroadcast++;//mi sposto da sinistra versop destra nel array broadcast
        }
        colonnaBroadcast=0;
        app=Memorizza.RestituisciOtteti(riga);//reset variabile di appoggio per gli otteti
        //Console.WriteLine($"Riga è: {riga}");
        Memorizza.MemorizzaBroadcast(riga, app, Memorizza.RestituisciRete(riga, app)+Memorizza.RestituisciValoreAggiunto(riga));//sommare valore rete con broadcast
        while(app<3)
        {
            Memorizza.MemorizzaBroadcast(riga, app+1, 255);//dove rimangono gli 0 riempire automaticamente con .255
            app++;
        }
        riga++;
    }
    public static void CalcoloRete()
    {
        int app=Memorizza.RestituisciOtteti(riga-1);//variabile di appoggio per gli otteti
        while(app>0)//app-1 trovo la parte di rete da ricopiare
        {
        Memorizza.MemorizzaRete(riga, colonnaBroadcast, Memorizza.Restituiscibroadcast(riga-1, colonnaBroadcast));//copia la parte di rete che non cambia
        app--;
        colonnaBroadcast++;//mi sposto da sinistra versop destra nel array broadcast
        }

        colonnaBroadcast=0;
        app=Memorizza.RestituisciOtteti(riga-1);//reset variabile di appoggio per gli otteti
        

        if(Memorizza.Restituiscibroadcast(riga-1, app)==255)
        {
            //Console.WriteLine($"IP broad in posizione: {app} è:{Memorizza.Restituiscibroadcast(riga-1, app)}");
            Memorizza.MemorizzaRete(riga, app-1, Memorizza.Restituiscibroadcast(riga-1, app-1)+1);//sommare valore rete con broadcast precedente al 255
        }
        else
        {
            Memorizza.MemorizzaRete(riga, app, Memorizza.Restituiscibroadcast(riga-1, app)+1);//sommare valore rete con broadcast
        }

        while(app<3)
        {
            if(Memorizza.Restituiscibroadcast(riga, app)==255)
            Memorizza.MemorizzaRete(riga, app, 0);//dove rimangono gli 0 riempire automaticamente con .255
            app++;
        }
    }
    static int rigaSubnet=0;
    public static void CalcoloSubnet()//work in progress
    {
        int app=Memorizza.RestituisciHost(0);
        double barra=0;
        do{
            double due55=0,subnet=0;
            int cont=0;

            barra=Memorizza.RestituisciBit(rigaSubnet);//capisco gli otteti interi
            for(;barra>=8;due55++)
            {
                barra-=8;
                cont++;
            }
            for(int i=0; due55>0; i++)
            {
                Memorizza.MemorizzaSubnet(rigaSubnet,i,255);//memorizzo i 255
                due55--;
            }
            int ind;
            for(ind=0; barra>0; ind++)
            {
                subnet+=Math.Pow(2,7-ind);
                barra--;
            }
            Memorizza.MemorizzaSubnet(rigaSubnet,cont,Convert.ToInt32(subnet));//memorizzo il valore di subnet convertito da double in int
            cont++;
            while(cont<4)
            {
                Memorizza.MemorizzaSubnet(rigaSubnet,cont,0);
                cont++;
            }
            rigaSubnet ++;
            app--;
        }while(app<0);
    }
}

class Memorizza
{
    static int[] Arrhost=new int[30];//array globale degli host
    static int Nhost=0;
    static int[] Arrbit=new int[30];//array globale dei bit 
    static int[] ArrayValore=new int[30];//array con valori da sommare alla rete
    static int[] ArrayContaOtteti=new int[30];//array con otteti su cui lavorare 
    static int[,] ArrRete=new int[30,4];//array globale delle reti
    static int[,] ArrBroadcast=new int[30,4];//array globale delle broadcast
    static int[,] ArraySubnetMask=new int[30,4];//array globale della subnet mask
    
//memorizza i valori

    public static void MemorizzaHost(int posizione, int host)
    {
        Memorizza.Arrhost[posizione]=host;//array per gli host
    }
    public static void MemorizzaBit(int posizione,int bit)
    {
        Arrbit[posizione]=bit;//memorizzo i bit nel array
        Calcolo.CalcoloMagicNumber(bit);//passo il bit per calcolare il magic number per trovare il broadcast
        Calcolo.CalcolOtteti(bit);//passo il bit per calcolare gli otetti su cui lavorare
    }

    public static void MemorizzaRete(int riga, int colonna, int ReteFrazionata)
    {
        ArrRete[riga,colonna]=ReteFrazionata;//memorizzo tutte le reti
    }

    public static void MemorizzaBroadcast(int riga, int colonna, int ReteFrazionata)
    {
        ArrBroadcast[riga, colonna]=ReteFrazionata;
    }

    public static void MemorizzaValoreAggiunto(int posizione, int ValoreAggiunto)
    {
        ArrayValore[posizione]=ValoreAggiunto;//memorizzo il valore da aggiungere
    }

    public static void MemorizzaOtteti(int posizione, int ContaOtteti)
    {
        ArrayContaOtteti[posizione]=ContaOtteti;
        //Console.WriteLine($"ContaOtteti sono: {ArrayContaOtteti[posizione]}");
    }

    public static void MemorizzaSubnet(int riga, int colonna, int valore)
    {
        ArraySubnetMask[riga,colonna]=valore;
        //Console.WriteLine($"L'altezza che entra è: {riga}");
    }

    public static int ContaHost(int quantita)
    {
        Nhost=Nhost+quantita;
        //Console.WriteLine($"ContaOtteti sono: {ArrayContaOtteti[posizione]}");
        return Nhost;
    }

//restituzione valori

    public static int RestituisciHost(int i)//funzione che restiruisce gli host
    {
        return Arrhost[i];
    }
    public static int RestituisciBit(int i)//funzione che restiruisce i bit
    {
        return Arrbit[i];
    }
    public static int RestituisciRete(int riga, int colonna)//funzione che restiruisce la rete
    {
        return ArrRete[riga, colonna];
    }
    public static int Restituiscibroadcast(int riga, int colonna)//funzione che restiruisce la rete
    {
        return ArrBroadcast[riga, colonna];
    }
    public static int RestituisciSubnet(int riga, int colonna)//restituisce la subnet 
    {
        return ArraySubnetMask[riga, colonna];
    }
    public static int RestituisciValoreAggiunto(int i)//restituisce il valore da sommare alla rete 
    {
        return ArrayValore[i];
    }
    public static int RestituisciOtteti(int i)//restituisce il valore da sommare alla rete 
    {
        return ArrayContaOtteti[i];
    }
    
}


class Impaginatura()
{
    public static void tabella()
    {
        int riga=0;
        Console.WriteLine(" ________________________________________________________________");
        Console.WriteLine("| Host | /Bit |      Rete      |   Broadcast   |      Subnet     |");//titolo
        Console.WriteLine("|------|------|----------------|---------------|-----------------|");
         for(int colonna=0; Memorizza.RestituisciHost(colonna)!=0; colonna++)
        {
            Console.Write($"| {Memorizza.RestituisciHost(colonna)} ");//stampo gli host in colonna

                if(Memorizza.RestituisciHost(colonna)>=100 && Memorizza.RestituisciHost(colonna)<1000)//allineamento tabella
                    Console.Write(" ");
            
                    else if(Memorizza.RestituisciHost(colonna)>=10 && Memorizza.RestituisciHost(colonna)<100)//**
                        Console.Write("  ");

                        else if(Memorizza.RestituisciHost(colonna)>=1 && Memorizza.RestituisciHost(colonna)<10)//**
                            Console.Write("   ");

            Console.Write("|");
            Console.Write($" /{Memorizza.RestituisciBit(colonna)}  | ");//stampo i bit in colonna
            int counter=0;
            for(int i=0; i<4 ; i++)
            {
              Console.Write(Memorizza.RestituisciRete(riga,i));//stampa la rete
              if(Memorizza.RestituisciRete(riga,i)<100 && Memorizza.RestituisciRete(riga,i)>=10)
                counter ++;
            else if(Memorizza.RestituisciRete(riga,i)<10)
                counter +=2;
              if(i<3)
                Console.Write(".");
            }
            for(;counter>0;counter--)
                Console.Write(" ");
            Console.Write("| ");
            counter=0;
            for(int i=0; i<4 ; i++)
            {
              Console.Write(Memorizza.Restituiscibroadcast(riga,i));//stampa il broadcast
              if(Memorizza.Restituiscibroadcast(riga,i)<100 && Memorizza.Restituiscibroadcast(riga,i)>=10)
                counter ++;
              else if(Memorizza.Restituiscibroadcast(riga,i)<10)
                counter +=2;
              if(i<3)
                Console.Write(".");
            }
            for(;counter>1;counter--)
                Console.Write(" ");
            counter=0;
            Console.Write("| ");
            for(int i=0; i<4 ; i++)
            {
              Console.Write(Memorizza.RestituisciSubnet(riga,i));//stampa la subnet
              if(Memorizza.RestituisciSubnet(riga,i)<100 && Memorizza.RestituisciSubnet(riga,i)>=10)
                counter ++;
              else if(Memorizza.RestituisciSubnet(riga,i)<10)
                counter +=2;
              if(i<3)
                Console.Write(".");
            }
            for(;counter>0;counter--)
                Console.Write(" ");
            counter=0;
            Console.Write(" | ");
            riga++;
            Console.WriteLine();//serve per andare a capo nella tabella
        }
        Console.WriteLine("|______|______|________________|_______________|_________________|");
    }
}

class Error()
{
    public static void Hostminori()
    {
        Console.WriteLine("Non è possibile avere host minore di 2");//messaggio di errore 
    }
    public static void HostOrdinati()
    {
        Console.WriteLine("Non è possibile avere host maggiore di quello precedente");//messaggio di errore
    }

static long Appoggio=5000000000;//variabile inizializzata cosi ai fini della comparazione iniziale
    public static int CondizioniHost()
    {
    int host=1;
        do{
            do{
                host=Convert.ToInt32(Console.ReadLine());
                if(host > Appoggio)//errore con host maggiore del precedente
                    Error.HostOrdinati();
            }while(host > Appoggio);
            
            if(host < 0 || host >=1 &&host<2)//errore con host minori di 2
                Error.Hostminori();
            else
                Appoggio=host;
            }while(host < 0 || host >1 &&host<2);
        return host;
    }
}

class Gatto()
{
    public static void StampaGatto()
    {
        Console.WriteLine("");Console.WriteLine("");
        Console.WriteLine("                      /\\         /\\");
        Console.WriteLine("                     /_ \\       /_ \\");
        Console.WriteLine("                    /____\\_____/____\\");
        Console.WriteLine("                   /                 \\");
        Console.WriteLine("                  |                   |");
        Console.WriteLine("                  |   O       O       |");
        Console.WriteLine("                  |       V           |__________________________________");
        Console.WriteLine("                   \\    \\_X_/        /                                   \\           __");
        Console.WriteLine("                    \\_______________/                                     \\         / /");
        Console.WriteLine("                                  \\                                      \\ \\       / /");
        Console.WriteLine("                                  |\\___________________________________| |\\ \\     / /");
        Console.WriteLine("                                  | |  | |                        | |  | | \\ \\___/ /");
        Console.WriteLine("                                  | |  | |                        | |  | |  \\_____/");
        Console.WriteLine("                                 _| | _| |                       _| | _| |");
        Console.WriteLine("                                |___||___|                      |___||___|");
    }
}
//ciao!