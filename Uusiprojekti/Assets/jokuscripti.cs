using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class jokuscripti : MonoBehaviour
{
    // Datatyyppien teko on mahdollista Struct -komennon avulla. Niissä on toiminnallisuutta.

    public struct Kysymys
    {

        // Määritetäänpä muuttujat.
        public string kysymysText;
        // Määritellään muuttujajoukkio.
        public string[] vastausvaihtoehdot;
        public int korrektiVastaus;

        // Nyt muodostamme konstruktorin.
        public Kysymys(string jokuKysymys, string[] jotkutVastaukset, int jokuOikeaVastaus)
        {
            this.kysymysText = jokuKysymys;
            this.vastausvaihtoehdot = jotkutVastaukset;
            this.korrektiVastaus = jokuOikeaVastaus;
        }
    }
    // Tässä kohtaa muodostamme uuden muuttujan eli luokan edellä mainitulla datatyypillä.
    public Kysymys uusiKysymys = new Kysymys("Minkä värinen on videopelisarjasta tuttu lohikäärme Spyro?", new string[] { "Vihreä", "Musta", "Purppura", "Punainen" }, 2);

    // Muodostetaan painikkeita varten oma joukko...
    public GameObject[] vastausNapit;
    //...jonka jälkeen tehdään luokka Text.
    public GameObject kyselyTeksti;

    // Tehkäämme joukko, jossa on 10 jäsentä Kysymys-tietotyypistä.
    private Kysymys[] multiKysely = new Kysymys[30];
    private int nykyinenKysymysIndex;
    public int[] kysymystenMaara;
    private int kysymyksetVastatut;

    // Seuraavaksi teemme joukon, joka pitää sisällään jo olemassa olevat paneelit.
    // Tämä sen takia, jotta ne voidaan piilottaa.
    public GameObject[] KysymysPaneelit;
    // Muuttuja, joka sisältää uuden paneelin, jotta se voidaan tuoda esille.
    public GameObject LopullinenTulos;
    // Muuttuja varsinaiselle tulokselle, eli tekstille.
    public Text tulosTeksti;
    // Muuttuja, joka laskee, kuinka monta vastausta meni oikein
    private int oikeidenVastaustenLkm;
    // Muuttuja palautetta varten.
    public GameObject palauteTeksti;

    // Start is called before the first frame update
    void Start()
    {
        //print(uusiKysymys.kysymysText + " oikea vastaus:" + uusiKysymys.vastausvaihtoehdot[1]);
        kysymystenMaara = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        

        multiKysely[0] = new Kysymys("Minkä värinen on videopelisarjasta tuttu lohikäärme Spyro?", new string[] { "Vihreä", "Musta", "Purppura", "Punainen" }, 2);
        multiKysely[1] = new Kysymys("Mikä maa voitti kultaa aikuisten jääkiekon MM-kisoissa 2019?", new string[] { "Ruotsi", "Kanada", "Venäjä", "Suomi" }, 3);
        multiKysely[2] = new Kysymys("Kuka voitti F1-mestaruuden vuonna 1984?", new string[] { "Alain Prost", "Niki Lauda", "Ayrton Senna", "Johnny Herbert" }, 1);
        multiKysely[3] = new Kysymys("Minkä bändin vokalisti Burton C. Bell on?", new string[] { "Fear Factoryn", "Static-X:n", "Sub Dub Micromachinen", "Crossbreedin" }, 0);
        multiKysely[4] = new Kysymys("Minkä niminen oli ensimmäinen Aku Ankan taskukirja?", new string[] { "Mikki kiipelissä", "Roope rahapulassa", "Apua Akulle", "Aku Taikaviitta" }, 0);
        multiKysely[5] = new Kysymys("Minkä maan pääkaupunki on Tbilisi?", new string[] { "Kirgisian", "Uzbekistanin", "Kazakstanin", "Georgian" }, 3);
        multiKysely[6] = new Kysymys("Mistä TV-ohjelmasta Yhdysvaltojen presidentti Donald Trump muistetaan?", new string[] { "Selviytyjät", "Diili", "Amazing Race", "Hell's Kitchen" }, 1);
        multiKysely[7] = new Kysymys("Missä maassa sijaitsee Varnan kultahietikko?", new string[] { "Unkarissa", "Romaniassa", "Bulgariassa", "Turkissa" }, 2);
        multiKysely[8] = new Kysymys("Milloin julkaistiin ensimmäinen Crash Bandicoot peli?", new string[] { "1997", "1995", "1998", "1996" }, 3);
        multiKysely[9] = new Kysymys("Kuinka paljon on 97 * 6?", new string[] { "522", "542", "562", "582" }, 3);
        multiKysely[10] = new Kysymys("Mitä tarkoittaa Aurora Borealis?", new string[] { "Meteorisadetta", "Tähdenlentoa", "Revontulia", "Auringonpimennystä" }, 2);
        multiKysely[11] = new Kysymys("Mikä oli viimeisimmän Putous-voittaja P. O. Liitikon yksi hokemista?", new string[] { "Kyllä kansa tietää!", "Kyllä kansa sietää!", "Kyllä kansa rientää!", "Kyllä kansa kieltää!" }, 1);
        multiKysely[12] = new Kysymys("Kuka oli päävihollinen Arrowin 4. kaudella?", new string[] { "Damien Darhk", "Ra's al Ghul", "Malcolm Merlyn", "Adrian Chase" }, 0);
        multiKysely[13] = new Kysymys("Lipussa on kolme raitaa, vasen raita on sininen, keskimmäinen raita valkoinen ja oikea raita punainen. Mikä maa on kyseessä?", new string[] { "Belgia", "Ranska", "Hollanti", "Monaco" }, 1);
        multiKysely[14] = new Kysymys("Kuka voitti viimeisimmän Selviytyjät Suomi -kauden?", new string[] { "Wallu", "Kim", "Lola", "Miska" }, 3);
        multiKysely[15] = new Kysymys("Kuka on Suomen presidentti?", new string[] { "Jukka Jalonen", "Vesa Keskinen", "Sauli Niinistö", "Jaajo Linnonmaa" }, 2);
        multiKysely[16] = new Kysymys("Minkä niminen on NHL-seura New York Islandersin maskotti?", new string[] { "Porky", "Sparky", "Doggy", "Hunter" }, 1);
        multiKysely[17] = new Kysymys("Minkä nimisen albumin Rammstein julkaisi vuonna 2019?", new string[] { "Ausländer", "Puppe", "Deutschland", "Rammstein" }, 3);
        multiKysely[18] = new Kysymys("Minkä rikoksen eurovaaliehdokas Näkkäläjärvi teki muun muassa nuoruudessaan?", new string[] { "Tappoi kissoja", "Ryösti kaupan", "Pahoinpiteli eläkeläisen", "Raiskasi nuoren tytön" }, 0);
        multiKysely[19] = new Kysymys("Kuka on kirjoittanut kirjan Tuntematon Kimi Räikkönen?", new string[] { "Jenni Pääskysaari", "Kari Hotakainen", "Tuomas Kyrö", "Miika Nousiainen" }, 1);
        multiKysely[20] = new Kysymys("11.9.2001 tapahtui jotain, mikä järisytti koko maailmaa. Mitä silloin tapahtui?", new string[] { "Tsunami iski Aasiaan", "Ydinpommeja tiputettiin Hiroshimaan ja Nagasakiin", "WTC-tornit romahtivat terrori-iskun seurauksena", "Juha Sipilä alasti Times Squarella" }, 2);
        multiKysely[21] = new Kysymys("Mitä tarkoittaa stadin slangilla bamlaus?", new string[] { "Oksentamista", "Ryyppämistä", "Kävelemistä", "Puhumista" }, 3);
        multiKysely[22] = new Kysymys("Kuka juonsi ensimmäiset Putous-kaudet?", new string[] { "Mikko Leppilampi", "Vappu Pimiä", "Aku Hirviniemi", "Jaakko Saariluoma" }, 3);
        multiKysely[23] = new Kysymys("Millä artistinimellä Karri Miettinen tunnetaan?", new string[] { "Elastinen", "Paleface", "Uniikki", "Pyhimys" }, 1);
        multiKysely[24] = new Kysymys("Miten jatkuu Turmion Kätilöiden biisin sanat: Pyörii lihamylly ja...?", new string[] { "Lehmän pylly", "Possun pylly", "Teurastajan pylly", "Mummon pylly" }, 2);
        multiKysely[25] = new Kysymys("Se on fifty-sixty, kuka sanoi näin aikanaan?", new string[] { "Matti Nykänen", "Seppo Räty", "Kimi Räikkönen", "Lasse Viren" }, 0);
        multiKysely[26] = new Kysymys("Missä maassa Euroviisut järjestettiin vuonna 2019?", new string[] { "Azerbaidzhanissa", "Venäjällä", "Virossa", "Israelissa" }, 3);
        multiKysely[27] = new Kysymys("Missä jalkapalloseurassa Teemu Pukki pelaa?", new string[] { "Derbyssä", "Nottinghamissa", "Norwichissä", "Leicesterissä" }, 2);
        multiKysely[28] = new Kysymys("Missä sijaitsevat espanjalaiset portaat?", new string[] { "Milanossa", "Roomassa", "Barcelonassa", "Madridissa" }, 1);
        multiKysely[29] = new Kysymys("Mikä seuraavista EI ole Toyotan automalli?", new string[] { "Corolla", "Primera", "Yaris", "Prius" }, 1);

        maaritaKysymys();
        liitaVastaukset(kysymystenMaara[0]);
    }
        // Update is called once per frame
        void Update()
    {
        pelinLoppu();
    }
    
    void liitaVastaukset(int kysymysnumero)
    {
        uusiKysymys = multiKysely[kysymysnumero];
        kyselyTeksti.GetComponent<Text>().text = uusiKysymys.kysymysText;
        for (int i = 0; i < vastausNapit.Length; i++)
        {
            vastausNapit[i].gameObject.GetComponentInChildren<Text>().text = uusiKysymys.vastausvaihtoehdot[i];
        }
    }
    public void tsekkaaVastaus(int painikeNumero)
    {
        if(painikeNumero == uusiKysymys.korrektiVastaus)
        {
            print("Hienoa!");
            oikeidenVastaustenLkm++;
            palauteTeksti.GetComponent<Text>().text = "Oikein!";
            palauteTeksti.GetComponent<Text>().color = Color.green;
        }
        else
        {
            print("Väärin meni.");
            palauteTeksti.GetComponent<Text>().text = "Väärin.";
            palauteTeksti.GetComponent<Text>().color = Color.red;
        }

        if (kysymyksetVastatut < kysymystenMaara.Length - 1)
        {
            palauteTeksti.SetActive(false);
            Thread.Sleep(1000);
            palauteTeksti.SetActive(true);
            siirrySeuraavaanKysymykseen();
            kysymyksetVastatut++;
        }
        else
        {
            foreach (GameObject p in KysymysPaneelit)
            {
                p.SetActive(false);
            }
            LopullinenTulos.SetActive(true);
            naytaVastaukset();
        }
    }
    void maaritaKysymys()
    {
        // Valitaan haluttu määrä kysymyksiä sattumanvaraisesti kysymysten joukosta.
        for (int i = 0; i < kysymystenMaara.Length; i++)
        {
            nykyinenKysymysIndex = Random.Range(0, multiKysely.Length);
            // Tarkistetaan, onko kysymys jo kysytty, jos ei, lisätään se mukaan.
            if(NumeroEiMukana(kysymystenMaara, nykyinenKysymysIndex))
            {
                kysymystenMaara[i] = nykyinenKysymysIndex;
            }
            // Jos kysymys on kysytty, looppiin lisätään yksi kierros, 
            // jotta uusi kysymys pystytään arpomaan.
            else
            {
                i--;
            }
        }
        nykyinenKysymysIndex = Random.Range(0, multiKysely.Length);
    }
    // Tämän apuohjelman tarkoituksena on käydä kysymykset läpi ja tarkastaa, onko
    // valittu kysymys jo mukana kysytyissä kysymyksissä.
    bool NumeroEiMukana(int[] numerot, int num)
    {
        for(int j = 0; j < numerot.Length; j++)
        {
            if(num == numerot[j])
            {
                return false;
            }
        }
        return true;
    }
    public void siirrySeuraavaanKysymykseen()
    {
        liitaVastaukset(kysymystenMaara[kysymystenMaara.Length - 1 - kysymyksetVastatut]);
    }

    public void naytaVastaukset()
    {
        switch (oikeidenVastaustenLkm)
        {
            case 15:
                tulosTeksti.text = "15/15 oikein! Tiesit kaiken!";
                break;
            case 14:
                tulosTeksti.text = "14/15 oikein! Aijai! Yhdestä kysymyksestä jäi kiinni!";
                break;
            case 13:
                tulosTeksti.text = "13/15 oikein! Melkein meni kaikki oikein!";
                break;
            case 12:
                tulosTeksti.text = "12/15 oikein! Melkein meni kaikki oikein!";
                break;
            case 11:
                tulosTeksti.text = "11/15 oikein! Hyvin tiedetty.";
                break;
            case 10:
                tulosTeksti.text = "10/15 oikein! Hyvin tiedetty.";
                break;
            case 9:
                tulosTeksti.text = "9/15 oikein! Hyvin tiedetty.";
                break;
            case 8:
                tulosTeksti.text = "8/15 oikein! Tiesit puolet vastauksista.";
                break;
            case 7:
                tulosTeksti.text = "7/15 oikein! Ei huono.";
                break;
            case 6:
                tulosTeksti.text = "6/15 oikein! Ei huono.";
                break;
            case 5:
                tulosTeksti.text = "5/15 oikein! Ei huono.";
                break;
            case 4:
                tulosTeksti.text = "4/15 oikein! Pystyt kyllä parempaankin.";
                break;
            case 3:
                tulosTeksti.text = "3/15 oikein! Pystyt kyllä parempaankin.";
                break;
            case 2:
                tulosTeksti.text = "2/15 oikein! Pystyt kyllä parempaankin.";
                break;
            case 1:
                tulosTeksti.text = "1/15 oikein! Vain yksi oikein???";
                break;
            case 0:
                tulosTeksti.text = "0/15 oikein! Ei ollut sinun päiväsi.";
                break;
            default:
                print("Nyt tapahtui jokin virhe!");
                break;
        }
    }

    public void aloitaPeliUudestaan()
    {
        SceneManager.LoadScene(Application.loadedLevelName);
    }

    void pelinLoppu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
