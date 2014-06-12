using UnityEngine;
using System.Collections;

public class KeywordsToSentences : MonoBehaviour {

	GameObject panel;
	Alice2 alice2;

	Hashtable keywordsNOW = new Hashtable();
	Hashtable keywordsCBS = new Hashtable();
	Hashtable keywordsD = new Hashtable();
	Hashtable keywordsNY = new Hashtable();
	Hashtable keywordsMOT = new Hashtable();

	GameObject labelBtnSentence;
	GameObject labelBtnKeyword;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();

		initializeHashtable();

		foreach (Transform child in this.transform)
		{
			if(child.name == "LabelBtnSentence")
			{
				labelBtnSentence = child.gameObject;
			} else if(child.name == "LabelBtnKeyword")
			{
				labelBtnKeyword = child.gameObject;
			}
		}
	}
	
	void Update () 
	{
		if(alice2.topicSet == "MARCHOFTIME")
		{
			labelBtnSentence.GetComponent<UILabel>().text = (string)keywordsMOT[labelBtnKeyword.GetComponent<UILabel>().text];
		} else if(alice2.topicSet == "CBS")
		{
			labelBtnSentence.GetComponent<UILabel>().text = (string)keywordsCBS[labelBtnKeyword.GetComponent<UILabel>().text];
		} else if(alice2.topicSet == "DIPLOMAT")
		{
			labelBtnSentence.GetComponent<UILabel>().text = (string)keywordsD[labelBtnKeyword.GetComponent<UILabel>().text];
		} else if(alice2.topicSet == "NEWYORKER")
		{
			labelBtnSentence.GetComponent<UILabel>().text = (string)keywordsNY[labelBtnKeyword.GetComponent<UILabel>().text];
		} else
		{
			labelBtnSentence.GetComponent<UILabel>().text = (string)keywordsNOW[labelBtnKeyword.GetComponent<UILabel>().text];

		}
	}

	void initializeHashtable()
	{
		//NOW
		keywordsNOW.Add("home"							, 	"Where is your home?");
		keywordsNOW.Add("call taxi" 					,	"Should I call you a taxi?");
		keywordsNOW.Add("here alone"					,	"Are you here on your own?");
		keywordsNOW.Add("address"						, 	"What is your address?");
		keywordsNOW.Add("name" 							,	"What is your name?");
		keywordsNOW.Add("Jena"							,	"Who is Jena?");
		keywordsNOW.Add("birthday"						,	"When is your birthday?");
		keywordsNOW.Add("year of birth"					,	"What year were you born?");
		keywordsNOW.Add("age"							,	"How old are you?");
		keywordsNOW.Add("siblings"						,	"Do you have any siblings?");
		keywordsNOW.Add("parents"						,	"Who are your parents?");
		keywordsNOW.Add("father"						,	"What did your father do?");
		keywordsNOW.Add("mother"						,	"What did your mother do?");
		keywordsNOW.Add("job"							,	"Where did you work?");
		keywordsNOW.Add("elementary school"				,	"Where did you go to elementary school?");
		keywordsNOW.Add("furniture elementary"			,	"What was the furniture at elementary school like?");
		keywordsNOW.Add("subjects elementary"			,	"What subjects did you have at elementary school?");
		keywordsNOW.Add("school uniform"				,	"What were the school uniforms like?");
		keywordsNOW.Add("favorite subject elementary"	,	"What was your favorite subject at elementary school?");
		keywordsNOW.Add("favorite teacher elementary"	,	"Who was your favorite teacher at elementary school?");
		keywordsNOW.Add("highschool"					,	"Where did you go to highschool?");
		keywordsNOW.Add("subjects highschool"			,	"What subjects did you have at highschool?");
		keywordsNOW.Add("favorite teacher highschool"	,	"Who was your favorite teacher at highschool?");
		keywordsNOW.Add("favorite subjects highschool"	,	"What was your favorite subject at high school?");
		keywordsNOW.Add("sports highschool"				,	"What sports did you do at highschool?");
		keywordsNOW.Add("hobbies highschool"			,	"What hobbies did you have during highschool?");
		keywordsNOW.Add("swimming team"					,	"Where you part of the swimming team?");
		keywordsNOW.Add("swimming style"				,	"What was your swimming style like?");
		keywordsNOW.Add("go out highschool"				,	"Did you go out a lot during highschool?");
		keywordsNOW.Add("college"						,	"Which college did you go to?");
		keywordsNOW.Add("course of studies"				,	"What was your course of study?");
		keywordsNOW.Add("graduated"						,	"When did you graduate?");
		keywordsNOW.Add("study finished"				,	"Did you finish your studies?");
		keywordsNOW.Add("grades college"				,	"What were your grades in college?");
		keywordsNOW.Add("hobbies college"				,	"What hobbies did you have during college?");
		keywordsNOW.Add("college parties"				,	"Did you go to college parties?");
		keywordsNOW.Add("favorite professor college"	,	"Who was your favorite college professor?");
		keywordsNOW.Add("pets college"					,	"Did you have any pets at college?");
		keywordsNOW.Add("first job"						,	"What was your first job?");
		keywordsNOW.Add("why study"						,	"Why did you study?");
		keywordsNOW.Add("college aim"					,	"What was your aim at college?");
		keywordsNOW.Add("march of time"					,	"What is the March of Time?");
		keywordsNOW.Add("henry luce"					,	"Who is Henry Luce?");
		keywordsNOW.Add("what research"					,	"What reasearch did you do?");
		keywordsNOW.Add("liked march of time"			,	"Did you like your job at the March of Time?");
		keywordsNOW.Add("how long at March of Time"		,	"How long did you work at the March of Time?");
		keywordsNOW.Add("working times March of Time"	,	"What were you working times at MOT?");
		keywordsNOW.Add("when March of Time"			,	"When did you work at the March of Time?");
		keywordsNOW.Add("pets March of Time"			,	"Did you have any pets while working for MOT?");
		keywordsNOW.Add("salary March of Time"			,	"What was your salary at MOT?");
		keywordsNOW.Add("holiday first job"				,	"How did you spend your holidays at MOT?");
		keywordsNOW.Add("name Kittens March of Time"	,	"What names did your Kittens have?");
		keywordsNOW.Add("flat first job"				,	"Where was your flat during your first job?");
		keywordsNOW.Add("boyfriend first job"			,	"Did you have a boyfriend during your first job?");
		keywordsNOW.Add("second job"					,	"What was your second job?");
		keywordsNOW.Add("quit first job"				,	"Why did you quit your first job?");
		keywordsNOW.Add("CBS"							,	"What is the CBS?");
		keywordsNOW.Add("What job at CBS"				,	"What did you do at CBS?");
		keywordsNOW.Add("liked CBS"						,	"Did you like working at CBS?");
		keywordsNOW.Add("working times CBS"				,	"What were your working times at CBS?");
		keywordsNOW.Add("pets CBS"						,	"What pets did you have while working at CBS?");
		keywordsNOW.Add("How long at CBS"				,	"How long did you work at CBS?");
		keywordsNOW.Add("go out CBS"					,	"Did you go out a lot during that time?");
		keywordsNOW.Add("culture CBS"					,	"What kind of culture did you enjoy at CBS?");
		keywordsNOW.Add("hobbies CBS"					,	"What were your hobbies while working at CBS?");
		keywordsNOW.Add("music CBS"						,	"What music did you listen to during that time?");
		keywordsNOW.Add("Jazz clubs"					,	"Did you go to a lot of Jazz clubs?");
		keywordsNOW.Add("opera CBS"						,	"Did you like the opera while working for CBS?");
		keywordsNOW.Add("third job"						,	"What was your third job?");
		keywordsNOW.Add("worked abroad"					,	"Where did you work abroad?");
		keywordsNOW.Add("after CBS"						,	"What did you do after CBS?");
		keywordsNOW.Add("job foreign service"			,	"What foreign services job did you do?");
		keywordsNOW.Add("diplomat"						,	"What did you do as a diplomat?");
		keywordsNOW.Add("working time third job"		,	"What were the working times for your third job?");
		keywordsNOW.Add("go out third job"				,	"Did you go out a lot during your third job?");
		keywordsNOW.Add("address third job"				,	"Where did you live during your third job?");
		keywordsNOW.Add("pets third job"				,	"Did you have any pets during your third job?");
		keywordsNOW.Add("Knatens"						,	"What are Knatens?");
		keywordsNOW.Add("street cats"					,	"Did you save a lot of street cats?");
		keywordsNOW.Add("fourth job"					,	"What was your fourth job?");
		keywordsNOW.Add("last job"						,	"What was your last job?");
		keywordsNOW.Add("New Yorker"					,	"Did you work for the New Yorker?");
		keywordsNOW.Add("How long at New Yorker"		,	"How long did you work for the New Yorker?");
		keywordsNOW.Add("What job at New Yorker"		,	"What did you do at the New Yorker?");
		keywordsNOW.Add("interviews New Yorker"			,	"Who did you interview at the New Yorker?");
		keywordsNOW.Add("liked job at New Yorker"		,	"Did you like your job at the New Yorker?");
		keywordsNOW.Add("Mr. Shawn"						,	"Who is Mr. Shawn?");
		keywordsNOW.Add("go out New Yorker"				,	"Did you go out a lot during your time at the New Yorker?");
		keywordsNOW.Add("stories at New Yorker"			,	"What stories did you write at the New Yorker?");
		keywordsNOW.Add("Javier Pereira"				,	"Who is Javier Pereira?");
		keywordsNOW.Add("preserve"						,	"What did you write about wildlife preservation?");
		keywordsNOW.Add("delicate"						,	"What did you write about delicate topics?");
		keywordsNOW.Add("When retired"					,	"When did you retire?");
		keywordsNOW.Add("pet"							,	"What pets do you have?");
		keywordsNOW.Add("hobbies"						,	"What hobbies do you have?");
		keywordsNOW.Add("opera"							,	"Do you like the opera?");
		keywordsNOW.Add("Venus"							,	"Who is Venus?");
		keywordsNOW.Add("music"							,	"What music do you listen to?");
		keywordsNOW.Add("Verdi"							,	"Who is Verdi?");
		keywordsNOW.Add("Wagner"						,	"Who is Wagner?");
		keywordsNOW.Add("go out"						,	"Did you go out a lot?");
		keywordsNOW.Add("live alone"					,	"Do you live alone?");
		keywordsNOW.Add("holiday"						,	"Where did you spend your holidays?");
		keywordsNOW.Add("What is important"				,	"What is most important for you?");
		keywordsNOW.Add("Saltaire"						,	"Did you go to Saltaire a lot?");
		keywordsNOW.Add("Fire Island"					,	"Did you go to the Fire Islands a lot?");
		keywordsNOW.Add("Cottage"						,	"You have a cottage?");

		//CBS
		keywordsCBS = keywordsNOW;
		keywordsCBS.Add("age 86"						, 	"Are you 86 years old?");
		keywordsCBS.Add("cities visited"				, 	"Which cities have you visited?");
		keywordsCBS.Add("favorite city"					, 	"What is your favorite city?");
		keywordsCBS.Add("Henry Luce"					, 	"Who is Henry Luce?");
		keywordsCBS.Add("howlong at mot"				, 	"How Long did you work at MOT?");
		keywordsCBS.Add("working times"					, 	"How were the working times at MOT?");
		keywordsCBS.Add("salary march of time"			, 	"What was your salary at MOT?");
		keywordsCBS.Add("name kittens mot"				, 	"What names did your kittens have?");
		keywordsCBS.Add("what research at cbs"			, 	"What research did you do at CBS?");
		keywordsCBS.Add("liked cbs"						, 	"Did you like your job at CBS?");
		keywordsCBS.Add("working times cbs"				, 	"How were the working times at CBS?");
		keywordsCBS.Add("pets cbs"						, 	"What pets did you have while working at CBS?");
		keywordsCBS.Add("how long at cbs"				, 	"How long did you work at CBS?");
		keywordsCBS.Add("go out cbs"					, 	"Did you go out a lot during that time?");
		keywordsCBS.Add("music cbs"						, 	"What music did you listen to during that time?");
		keywordsCBS.Add("jazz clubs"					, 	"Did you go to a lot of Jazz clubs?");
		keywordsCBS.Add("opera cbs"						, 	"Did you go to the opera a lot during your time at CBS?");
		keywordsCBS.Add("foreign service"				, 	"Where did you do your foreign service?");
		keywordsCBS.Add("how long at ny"				, 	"How long have you lived in New York?");
		keywordsCBS.Add("like job ny"					, 	"Do you like working in New York?");
		keywordsCBS.Add("when retired"					, 	"When did you retire?");
		keywordsCBS.Add("why go out "					, 	"Why did you go out?");
		keywordsCBS.Add("career impotrant"				, 	"How important is your career for you?");
		keywordsCBS.Add("fire island"					, 	"Did you go to the Fire Islands a lot?");
		keywordsCBS.Add("cottage"						, 	"Do you have a cottage?");
		keywordsCBS.Add("what animals"					, 	"What animals do you have?");
		keywordsCBS.Add("Giselda and Daisy"				, 	"Who are Giselda and Daisy?");
		keywordsCBS.Add("Mini Cinderella"				, 	"Who are Mini and Cinderella?");

		//D
		keywordsD = keywordsCBS;
		keywordsD.Add("abroad holiday"		 			,	"Do you travel abroad during holidays?");
		keywordsD.Add("working time diplomat" 			,	"What were your working times as a diplomat?");
		keywordsD.Add("go out diplomat" 				,	"Did you go ou a lot as a diplomat?");
		keywordsD.Add("address diplomat" 				,	"What was your address as a diplomat?");
		keywordsD.Add("knatens" 						,	"What are Kantens?");
		keywordsD.Add("pets trieste" 					,	"Did you have any pets in Trieste?");
	
		//NY
		keywordsNY = keywordsD;
		keywordsNY.Add("new yorker"			 			,	"When did you work for the New Yorker?");
		keywordsNY.Add("what writer"			 		,	"What did you do as a writer?");
		keywordsNY.Add("interviews"						,	"Who did you interview?");
		keywordsNY.Add("mr. shawn"			 			,	"Who is Mr. Shawn?");
		keywordsNY.Add("after mr. shawn"			 	,	"What happend after Mr. Shawn left?");
		keywordsNY.Add("stories at ny"			 		,	"What stories did you write at the New Yorker?");
		keywordsNY.Add("javier pereira"			 		,	"Who was Javier Pereira?");
		keywordsNY.Add("go out new yorker"				,	"Did you go out a lot while working for the New Yorker?");

		//MOT
		keywordsMOT = keywordsNY;
		keywordsMOT.Add("like march of time"	 		,	"Did you like working at March of Time?");
		keywordsMOT.Add("kittens"				 		,	"How are your kittens called?");
		keywordsMOT.Add("cat color"			 			,	"What colors are your cats?");
	}
}