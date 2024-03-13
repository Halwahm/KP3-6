const knowledge = [
 
];

//для хранения псевдоокончаний сказуемых 
let endings =
[
    ["ет","(ет|ут|ют)"], 
    ["ут","(ет|ут|ют)"], 
    ["ют","(ет|ут|ют)"],		     //1 спряжение

    ["ит","(ит|ат|ят)"], 
    ["ат","(ит|ат|ят)"], 
    ["ят","(ит|ат|ят)"],		     //2 спряжение

    ["ется","(ет|ут|ют)ся"], 
    ["утся","(ет|ут|ют|ующие)ся"], 
    ["ются","(ет|ут|ют)ся"], //1 спряжение, возвратные

    ["ится","(ит|ат|ят)ся"], 
    ["атся","(ит|ат|ят)ся"], 
    ["ятся","(ит|ат|ят)ся"],     //2 спряжение, возвратные

    ["ящие","ящие"],
    ["ee", "ee"],
    ["ен","ен"],
    ["ую","ая"],
    ["му","ма"],
    ["ена","ена"], 
    ["ено","ено"], 
    ["ены","ены"],		    //краткие прилагательные

    ["ан","ан"], 
    ["ая","ую"],
    ["ана","ана"], 
    ["ано","ано"], 
    ["аны","аны"],		    //краткие прилагательные

    ["но", "но"], 
    ["на", "на"],

    ["жен","жен"], 
    ["жна","жна"], 
    ["жно","жно"], 
    ["жны","жны"],

    ["такое", "- это"],
]

//«черный» список подлежащих
let blacklist = ["замена", "замены", "атрибут", "маршрут", "член", "нет"]

//определения сказуемых в вопросе по псевдоокончаниям
function getEndingIndex(word)
{
    //проверка слова на совпадение по черному списку в массиве blacklist
    if(blacklist.indexOf(word) !== -1)
        return -1;
    
    //перебор псевдоокончаний в массиве endings
    for(let i = 0; i < endings.length; i++)
        //проверка, оканчивается ли слово word на iE-ое псевдоокончание
        if(word.substring(word.length - endings[i][0].length) == endings[i][0])     // substring(начальный индекс, конечный)
            //возврат номера найденного псевдоокончания для сказуемого 
            return i;

    //если совпадений нет, то возврат -1
    return -1;
} // пример: такое (5) - ет (2) == ет

//Функция, которая делает первую букву в тексте маленькой:
function firstSymbolToLowerCase(str)
{
    return str.substring(0, 1).toLowerCase() + str.substring(1);
}

//Функция, которая делает первую букву в тексте большой:
function firstSymbolToUpperCase(str)
{
    return str.substring(0, 1).toUpperCase() + str.substring(1);
}

function getAnswer(question)
{
    //преобразование текста из параметра функции question 
    //чтобы сделать первую букву в тексте вопроса прописной (масенькой)
    let text = firstSymbolToLowerCase(question);
    //знаки препинания
    let separators = "'\",.!?()[]\\/";
    //добавление пробелов перед знаками препинания
    for(let i = 0; i < separators.length; i++)
        text = text.replace(separators[i], " " + separators[i]);    // replace(что заменить, на что заменить)

    //массив слов и знаков препинания, отделенных пробелами
    let words = text.split(' ');

    //перебор слов в массиве слов из вопроса: первый символ каждого слова к lowerCase
    for(let i = 0; i < words.length; i++)
        words[i] = firstSymbolToLowerCase(words[i]);

    //флаг, найден ли ответ на вопрос
    let result = false;
    let answer = "";

    for(let i = 0; i < words.length; i++)
    {
        // поиск номера псевдоокончания сказуемого
        let ending = getEndingIndex(words[i]);

        if(ending >= 0)
        {
            //замена псевдоокончания на набор возможных окончаний, хранящихся 
            //во втором столбце массива. пример, глагол "ведет"(5) - endings[0][0].length (ет(2)). substring(0, 3) = вед + (ет|ут|ют) = вед(ет|ут|ют)

            words[i] = words[i].substring(0, words[i].length - endings[ending][0].length) + endings[ending][1]

            console.log(words)
            
            //создание регулярного выражения для поиска по сказуемому из вопроса
            let predicate = new RegExp(".*" + words[i] + ".*");     // .*: любой символ (кроме новой строки) ноль или более раз.

            //для кратких прилагательных нужно захватить следующее за найденным слово
            if(endings[ending][0] == endings[ending][1])
            {
                if(words[i] != "нужен")
                {
                    predicate = new RegExp(".*" + words[i] + " " + words[i + 1] + ".*");    // захватываем следующее за ним слово
                    i++;
                }
            }
            console.log(predicate)  

            //создание регулярного выражения для поиска по подлежащему из вопроса. пример «Как рассчитывается площадь квадрата» после сказуемого будет сформировано 
            //регулярное выражение  для подлежащего: «.*площадь.*квадрата.*», 

            let subjectReg = words.slice(i + 1).join(".*");
            //words.slice(i + 1): Метод slice используется для создания нового массива, начиная с элемента i + 1 и до конца массива words. Он возвращает подмассив, не изменяя 
            //оригинальный массив.

            //.join(".*"): Метод join используется для объединения элементов массива в строку с указанным разделителем. В данном случае, элементы массива будут 
            //объединены с использованием строки ".*" в качестве разделителя.
            console.log(subjectReg)
            
            //только если в подлежащем больше трех символов
            if(subjectReg.length > 3)
            {
                let subject = new RegExp(".*" + subjectReg + ".*");
                console.log(subject)

                //поиск совпадений с шаблонами среди связей семантической сети
                for(let j = 0; j < knowledge.length; j++)
                {
                    if(predicate.test(knowledge[j][1].toLowerCase()) && (subject.test(knowledge[j][0].toLowerCase()) || subject.test(knowledge[j][2].toLowerCase())))
                    {
                        //создание простого предложения из семантической связи
                        answer += firstSymbolToUpperCase(knowledge[j][0] + " " + knowledge[j][1] + " " + knowledge[j][2] + "<br>");
                        result = true;
                    }
                }

                //если совпадений с двумя шаблонами нет,
                if(!result)
                {
                    //поиск совпадений только с шаблоном подлежащего
                    for(let j = 0; j < knowledge.length; j++)
                    {
                        if(subject.test(knowledge[j][0].toLowerCase() || subject.test(knowledge[j][2].toLowerCase())))
                        {
                            //создание простого предложения из семантической связи
                            answer += firstSymbolToUpperCase(knowledge[j][0] + " " + knowledge[j][1] + " " + knowledge[j][2] + "<br>");
                            result = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    //если ответа нет
    if(!result)
        answer = "Ответ не найден. <br/>";
    return answer;
}
    
const questions = [
  
];


function Script() {
    for (let i = 0; i < questions.length; i++) {
        if (confirm("Продолжить диалог?")) {
            let userQuestion = prompt("Введите вопрос", questions[i]);
            alert(getAnswer(userQuestion));
        } else {
            break;
        }
    }
}
            
var dialogOn = false;

function dialog_window() {
  document.body.innerHTML += "<div id='dialog' class='dialog' style='margin-left:-25px;'>" + 
  "<div class='label' onclick='openDialog()'>Нажимай сюда😜</div>" +
  "<div class='header'>История</div>" +
  "<div class='history' id='history'></div>" +
  "<div class='question'><input id='Qdialog' placeholder='Введите вопрос'/> <br><button onclick='ask(\"Qdialog\")'>Спросить</button></div>" + "<button class='button-quest' onclick='clearHistory()'>Очистить всю историю</button>" +
  "</div>";
  //API-ключ для подключения к речевому сервису Yandex
  window.ya.speechkit.settings.apikey='5c6d6536-b453-4589-9bc7-f16c7a795106';
  //добавление элемента управления "Поле для голосового ввода"
  //с выводом распознанной речи в строку ввода текста
  var textline = new ya.speechkit.Textline('Qdialog',{onInputFinished:function(text){
  document.getElementById('Qdialog').value=text;}});
}

dialog_window();

function openDialog(){
  if(dialogOn) {  		//анимация закрытия диалогового окна
      $("#dialog").animate({"margin-left":"-25px"},1000,function(){});
          dialogOn=false;
  }
  
  else {				//анимация открытия диалогового окна
      $("#dialog").animate({"margin-left":"-1180px"},1000,function() {});
          dialogOn=true;
  clearInterval(timer);
  }
}

function ask(questionInput){
  //переменная для считывания содержания окна ввода вопроса:
  var question=document.getElementById(questionInput).value;
  //активизация диалога
  dialogOn=true;
  //создаем переменную и сохраняем в ней тег <div> 
  var newDiv=document.createElement("div");
  //задаем класс оформления созданного блока
  newDiv.className='question';
  //заполняем созданный блок текстом вопроса
  newDiv.innerHTML=question;
  //вставляем созданный блок в блок <history> и закрываем его
  document.getElementById("history").appendChild(newDiv);
  +"</div>";
  //создаем еще один блок <div>
  newDiv=document.createElement("div");
  //задаем класс оформления созданного блока 
  newDiv.className='answer';
  //наполняем созданный блок ответом, полученным от функции getAnswer()
  newDiv.innerHTML=getAnswer(question);
  //добавляем в ответ тег аудио, ссылающийся на звук от синтезатора Yandex
  newDiv.innerHTML+=
  "<audio controls='true' autoplay='true'src='http://tts.voicetech.yandex.net/ key=5c6d6536-b453-4589-9bc7-f16c7a795106 text="
  +newDiv.innerText+"'></audio>";
  //в котором указывается формат звука и язык озвучиваемого текста generate?format=wav&lang=ru-RU&
  //а также ключ доступа к SpeechKit Cloud API 
  //озвучиваемый текст, который берется из сгенерированного ответа
  //вставка зввукового блока в блок <history>
  document.getElementById("history").appendChild(newDiv);
  //запуск звука
  if(newDiv.lastChild.tagName=="AUDIO"){newDiv.lastChild.play();	}
  //прокрутка в окне истории ответов в самый низ
  document.getElementById("history").scrollTop = 
          document.getElementById("history").scrollHeight;	
  //очистка текстового поля для ввода нового вопроса
  document.getElementById(questionInput).value="";
}            
  
function clearHistory() {
    var historyDiv = document.getElementById("history");
    historyDiv.innerHTML = ''; 
}
                