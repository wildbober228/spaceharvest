using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class auto_station : MonoBehaviour {

    public GameObject obj;//сам префаб
    [Header("стороны монтажа")]
    public Transform[] tubes;//стороны монтажа
    public Transform[] tubes2;//стороны монтажа
    public Transform[] tubes_help;//стороны монтажа
    [Header("----------------------------")]
    
    [Header("булы что это такое")]
    public bool angar_b;
    public bool start_position;
    public bool karkas;
    [Header("----------------------------")]
    [Header("help booleans")]
    public bool can_spawn = true;
    public bool spawn_last_karkas = true;
    public bool crash = false;
    public bool setup = false;
    public bool help_bool_not_random = true;
    [Header("----------------------------")]
    [Header("Строны модулей")]
    public int a;
    public int b;
    public int storona = 8;
    [Header("----------------------------")]
    [Header("help ints")]
    public int kolvo_moduls = 0;
    public int kolvo_moduls_not_r = 0;
    public int kolvo_moduls_not_r_help = 0;
    public int kolvomod_help = 0;
    public int kolvo_mod_max;
    public int kolvooo = 0;
    [Header("----------------------------")]
    [Header("type of modul")]
    public int type;
    [Header("----------------------------")]
    /*
  Типы=
  0)модель жизне обеспечения
  1)куб с окнами
  */
    [Header("type of station")]
    public int type_of_station = -1;   
    [Header("----------------------------")]
    /*
  Типы=
  0)добывающая
  1)склад
  2)военная
  3)перерабатывающая
  4)склад
  */
    [Header("spawn modules")]
    public GameObject start_pos;
    public GameObject angar;
    public GameObject[] Important_Moduls;
    public GameObject[] station_moduls; //0 - станционный бур
    [Header("----------------------------")]
    [Header("ссылка на след блок")]
    public GameObject prev_modul;
    public GameObject next_modul;
    [Header("----------------------------")]
    [Header("Вспомогательные обьекты")]
    public GameObject find_tub;
    [Header("----------------------------")]
    [Header("ТОЛЬКО ДЛЯ ГЛАВНОГО МОДУЛЯ")]

 
    [Header("Прочность всего корабля")]
    public float health = 1;
    [Header("Прочность Модуля")]
    public float health_modul = 1;
    [Header("Размер Грузового Отсека")]
    public float cargo = 1;
    [Header("Кол-во Патронов")]
    public float ammo = 1;
    [Header("Кол-во Энергии")]
    public float energy = 1;
    [Header("Требуется Энергии на Подпитку Корабля")]
    public float energy_need = 1;
    [Header("Накопление ЦПУ")]
    public float cpu_regen = 1;
    [Header("Требуется ЦПУ на Подпитку Корабля")]
    public float cpu_need = 1;
    [Header("Мин Скорость Корабля")]
    public float speed_min = 0;
    [Header("Текущая Скорость Корабля")]
    public float speed_now = 0;
    [Header("Макс Скорость Корабля")]
    public float speed_max = 1;
    [Header("Скорость Поворота")]
    public float rotate_speed = 1;
    [Header("Кол-во Экипажа")]
    public int   amount_of_people = 1;
    [Header("Кол-во Экипажа")]
    public int need_helth_modul = 1;
    [Header("Эффективность копания руды")]
    public int eff_mining = 0;
    [Header("----------------------------------------")]
    [Header("Для добычи руды")]

    public GameObject[] asteroids;
    public GameObject Miner;


    [Header("----------------------------------------")]
    [Header("Переменные для создания станции по seed")]
    public int tub;

    public string help_ss;
    public string help_ss_im;
    public string help_ss_im_type;
    public bool not_rand;
    public int index = 0;
    public string Modul_help_name_r = "modul18";
    public string Modul_help_name;
    public int digital_coner;
    public int type_of_station_not_r;
    [Header("----------------------------------------")]
    [Header("Текущий seed")]
    public int[] seed = { 2, 5, 0, 5, 4, 4, 3, 3, 4, 0, 5, 0, 5, 2, 4, 4, 4, 0, 0, 3, 0, 4, 0, 0, 0 };
    public int[] seed_i = { 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3, 3, };
    public int[] seed_impot_type = { 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, };
    public int[] par_of_station_modul = { 3,0 };

    void Start () {
       
       // seed = { 4, 3, 0, 2, 0, 3, 0, 0, 4, 0, 4, 2, 5, 5, 0, 2, 2, 1, 5, 2, 1, 0, 0, 4 };
       if(karkas == true || start_position == true)
        can_spawn = true;
        if (not_rand == true && can_spawn == true)
        {
            kolvo_moduls_not_r_help = start_pos.GetComponent<auto_station>().kolvo_moduls_not_r;
            if (start_position != true)
            {
                index = start_pos.GetComponent<auto_station>().index;              
            }
        }
        Debug.Log("summon");
        if (start_position == true)
        {
            type_of_station = Random.Range(0, 1);
            type_of_station_not_r = type_of_station;
            Debug.Log("Start");
            if(type_of_station == 0)
            {
                asteroids = GameObject.FindGameObjectsWithTag("asteroid");
            }
               
        }
        if (start_position != true && angar_b == false && not_rand == false)
        {
            kolvomod_help = start_pos.GetComponent<auto_station>().kolvo_moduls;
           
            kolvo_mod_max = start_pos.GetComponent<auto_station>().kolvo_mod_max;
        }
        
    }
	//как будет работать - 
    /*
    1)Проверка на важные модули
    2)Выбор класса станции
    3)Проверка опасности системы
    4)В зависимости от системы установка до оборудования по классам
    5)Мб добавение украшений
    */
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < 5; i++) ;

        if(type_of_station == 0 && Miner!= null && start_position == true)
        {
            if(asteroids.Length > 0)
            {
                Miner.transform.LookAt(asteroids[0].transform);
            }
        }

        if(not_rand == true && can_spawn == true)
        {   if (index != start_pos.GetComponent<auto_station>().seed.Length && index != start_pos.GetComponent<auto_station>().seed_i.Length)
            {
                if (start_position == true)
                {
                    a = seed[0];
                    b = seed_i[0];
                    type = seed_impot_type[0];
                    spawn_not_rand(a);
                    spawn_not_rand_impor_mod(b, type);
                }
                else
                {
                    a = seed[index];
                    b = seed_i[index];
                    type = seed_impot_type[index];
                    spawn_not_rand(a);
                    spawn_not_rand_impor_mod(b,type);
                }
            }
        if(index == start_pos.GetComponent<auto_station>().seed.Length && help_bool_not_random == true)
            {
                help_bool_not_random = false;
                if (type_of_station == par_of_station_modul[1])
                {
                    GameObject go1 = (GameObject)Instantiate(station_moduls[0], GameObject.Find(start_pos.GetComponent<auto_station>().Modul_help_name_r).GetComponent<auto_station>().tubes[par_of_station_modul[0]].position, obj.transform.rotation);
                 
                    go1.name = "station_miner";
                    start_pos.GetComponent<auto_station>().Miner = go1;
                    go1.GetComponent<auto_station>().prev_modul = GameObject.Find(start_pos.GetComponent<auto_station>().Modul_help_name_r);
                    go1.GetComponent<auto_station>().start_pos = prev_modul.GetComponent<auto_station>().start_pos;
                    start_pos.GetComponent<auto_station>().eff_mining++;
                }
            }
        }


       if(not_rand == false)
        if(kolvomod_help+1 < kolvo_mod_max)
		if(tubes!= null && can_spawn == true)
        {
            a = Random.Range(0, 6);
            int help = 8;
            switch (storona)
            {
                case 0:
                    help = 1;
                    break;
                case 1:
                    help = 0;
                    
                    break;
                case 2:
                    help = 3;
                    break;
                case 3:
                    help = 2;
                    break;
                case 4:
                    help = 5;
                    break;
                case 5:
                    help = 4;
                    break;
            }

                if (a == help && angar_b == false)
                {
                    
                        a = Random.Range(0, 6);
                        b = Random.Range(0, 6);
                        type = Random.Range(0, 2);
                    
                   
                       

                    
                   
                }
              
                spawn(a);
              //  if(can_spawn == true)
                spawn_impo_mod(b,type);
              
            }
        if (type_of_station == 0 && start_position == true)
            if (start_pos.GetComponent<auto_station>().kolvo_moduls + 2 == start_pos.GetComponent<auto_station>().kolvo_mod_max && setup == false && this.gameObject.tag == "karkas")
            {
                setup = true;
                Debug.Log("Tryba= " + this.gameObject.name);
                bb: tub = Random.Range(2, start_pos.GetComponent<auto_station>().kolvo_mod_max - 2);//номер модуля
                if (tub % 2 != 0) goto bb;
                find_tub = GameObject.Find("modul" + tub);
                b: int a_new = Random.Range(0, 6);

                if (a_new != a && find_tub.GetComponent<auto_station>().tubes[a_new] != null && find_tub.GetComponent<auto_station>().tubes2[a_new] != null)
                {
                    start_pos.GetComponent<auto_station>().digital_coner = a_new;
                    start_pos.GetComponent<auto_station>().Modul_help_name = find_tub.name;
                    find_tub.GetComponent<auto_station>().tubes[a_new].gameObject.active = true;
                    find_tub.GetComponent<auto_station>().tubes2[a_new].gameObject.active = true;

                    GameObject go1 = (GameObject)Instantiate(station_moduls[0], find_tub.GetComponent<auto_station>().tubes[a_new].position, obj.transform.rotation);
                    go1.name = "station_miner";
                    go1.GetComponent<auto_station>().prev_modul = find_tub;
                    go1.GetComponent<auto_station>().start_pos = prev_modul.GetComponent<auto_station>().start_pos;
                    start_pos.GetComponent<auto_station>().eff_mining++;
                }
                else goto b;
            }
        if (kolvooo == 0 && start_position == false && spawn_last_karkas == true && angar_b == false)
        {
            spawn_last_karkas = false;
            GameObject go1 = (GameObject)Instantiate(angar, tubes[a].position, obj.transform.rotation);
            go1.name = "dock";
            next_modul = go1;
            Destroy_Tubes(100);
        }
        if (start_position == false && angar_b == false)
        {
            if (prev_modul == null && next_modul == null && crash == false)
            {
                crash = true;
                this.gameObject.AddComponent<Rigidbody>().AddForce(this.transform.position * 3);
            }
         
        }
    }
    void spawn_impo_mod(int b,int type)
    {
        if (b < tubes.Length)
        {
            if (start_position == false)
            {
                start_pos.GetComponent<auto_station>().help_ss_im_type += type + ",";
                if (type == 0)
                {
                 start_pos.GetComponent<auto_station>().need_helth_modul++;
                }
                if (type == 1)
                {
                 start_pos.GetComponent<auto_station>().amount_of_people++;
                }
            }

            GameObject go1 = (GameObject)Instantiate(Important_Moduls[type], tubes[b].position, obj.transform.rotation);
            go1.name = "sys" + kolvomod_help;
            go1.GetComponent<auto_station>().storona = b;
            go1.GetComponent<auto_station>().prev_modul = this.gameObject;
            
           // next_modul = go1;
            if (start_position != true)
            {
                start_pos.GetComponent<auto_station>().kolvo_moduls++;
                go1.GetComponent<auto_station>().start_pos = start_pos;
                start_pos.GetComponent<auto_station>().help_ss_im += b + ",";
            }
            if (start_position == true)
            {
                go1.GetComponent<auto_station>().start_pos = start_pos;
                prev_modul = this.gameObject;
            }

            kolvooo++;

        }
     //   estroy_Tubes(2);
    }
    void spawn_station_moduls(GameObject mod)
    {


    }
    void spawn(int a)
    {
        can_spawn = false;
        if (a < tubes.Length)
        {
           
            
            GameObject go1 = (GameObject)Instantiate(obj, tubes[a].position,obj.transform.rotation);
            go1.name = "modul" + kolvomod_help;
            go1.GetComponent<auto_station>().storona = a;
            go1.GetComponent<auto_station>().prev_modul = this.gameObject;
            
            next_modul = go1;
            if (start_position != true)
            {
                start_pos.GetComponent<auto_station>().kolvo_moduls++;
                start_pos.GetComponent<auto_station>().kolvo_moduls_not_r++;
                go1.GetComponent<auto_station>().start_pos = start_pos;
                start_pos.GetComponent<auto_station>().help_ss += a +",";
            }
            if (start_position == true)
            {
                go1.GetComponent<auto_station>().start_pos = start_pos;
                go1.GetComponent<auto_station>().not_rand = start_pos.GetComponent<auto_station>().not_rand;
               prev_modul = this.gameObject;
            }

            kolvooo++;

        }
        Destroy_Tubes(a);
    }
    void spawn_not_rand(int a)
    {
        can_spawn = false;
        
            GameObject go1 = (GameObject)Instantiate(obj, tubes[a].position, obj.transform.rotation);

            go1.name = "modul" + kolvo_moduls_not_r_help;

            go1.GetComponent<auto_station>().storona = a;
            go1.GetComponent<auto_station>().prev_modul = this.gameObject;
            next_modul = go1;
            if (start_position != true)
            {
                go1.GetComponent<auto_station>().start_pos = start_pos;
                start_pos.GetComponent<auto_station>().kolvo_moduls_not_r++;
                start_pos.GetComponent<auto_station>().help_ss += a + ",";
                start_pos.GetComponent<auto_station>().index++;
            }
            if (start_position == true)
            {
                //start_pos.GetComponent<auto_station>().help_ss += a + ",";
                go1.GetComponent<auto_station>().start_pos = start_pos;
                go1.GetComponent<auto_station>().not_rand = start_pos.GetComponent<auto_station>().not_rand;
                prev_modul = this.gameObject;
            }
            kolvooo++;
        
        Destroy_Tubes(a);
    }
    void spawn_not_rand_impor_mod(int b, int type)
    {
       
            if (start_position == false)
            {
                if (type == 0)
                {
                    start_pos.GetComponent<auto_station>().need_helth_modul++;
                }
                if (type == 1)
                {
                    start_pos.GetComponent<auto_station>().amount_of_people++;
                }
            }

            GameObject go1 = (GameObject)Instantiate(Important_Moduls[type], tubes[b].position, obj.transform.rotation);
            go1.name = "sys" + kolvo_moduls_not_r_help;
            go1.GetComponent<auto_station>().storona = b;
            go1.GetComponent<auto_station>().prev_modul = this.gameObject;
            // next_modul = go1;
            if (start_position != true)
            {
                start_pos.GetComponent<auto_station>().kolvo_moduls_not_r++;
            start_pos.GetComponent<auto_station>().help_ss_im += b + ",";
            go1.GetComponent<auto_station>().start_pos = start_pos;
            }
            if (start_position == true)
            {
                go1.GetComponent<auto_station>().start_pos = start_pos;
            go1.GetComponent<auto_station>().not_rand = start_pos.GetComponent<auto_station>().not_rand;
            prev_modul = this.gameObject;
            }

            kolvooo++;

        
    }
        void Destroy_Tubes(int a)
    {
        if (a < tubes2.Length)
        {
            for (int i = 0; i < tubes2.Length; i++)
            {
                if (tubes2[i] != tubes2[a] && tubes2[i] != tubes2[b])
                    tubes_help[i] = tubes2[i];

            }
            if(tubes_help.Length > 0)
            for (int i = 0; i < tubes_help.Length; i++)
            {
                if(tubes_help[i] != null)
                    tubes_help[i].gameObject.active = false;
                }
        }
        else
        {
            for (int i = 0; i < tubes2.Length; i++)
            {

                // Destroy(tubes2[i].gameObject);
                tubes2[i].gameObject.active = false;
            }

        }
    }
}
