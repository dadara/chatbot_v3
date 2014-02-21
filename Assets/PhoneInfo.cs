using UnityEngine;
using System.Collections;

public class PhoneInfo : MonoBehaviour {

	GameObject panel;
	Alice2 alice2;

	// Use this for initialization
	void Start () 
	{
		panel = GameObject.Find("Panel");
		alice2 = panel.GetComponent<Alice2>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(alice2.topicSet == "")
		{
			this.transform.GetComponent<UILabel>().text = "Not Available";
		} else if(alice2.topicSet == "MARCHOFTIME")
		{
			this.transform.GetComponent<UILabel>().text = "THE MARCH OF TIME:" + "\n" + "\n" + "The March of Time is an American radio news series broadcast from 1931 to 1945, and a companion newsreel series shown in movie theaters from 1935 to 1951. Created by broadcasting pioneer Fred Smith and Time magazine executive Roy E. Larsen, the program combined actual news events with reenactments. The 'voice' of The March of Time was Westbrook Van Voorhis. The radio program was later developed into a newsreel series produced and written by Louis de Rochemont and his brother Richard de Rochemont. The March of Time was recognized with an Academy Honorary Award in 1937.The March of Time organization also produced four feature films for theatrical release, and created documentary series for early television. Its first TV series, Crusade in Europe (1949), received a Peabody Award and one of the first Emmy Awards.";
		} else if(alice2.topicSet == "CBS")
		{
			this.transform.GetComponent<UILabel>().text = "CBS:" + "\n" + "\n" + "CBS (corporate name CBS Broadcasting, Inc.) is an American commercial broadcast television network, which started as a radio network; it continues to operate a radio network and a portfolio of television and radio stations in large and mid-sized markets. The name is derived from the initials of the network's former name, Columbia Broadcasting System. It is the second largest broadcaster in the world behind the British Broadcasting Corporation. The network is sometimes referred to as the 'Eye Network', in reference to the shape of the company's logo. It has also been called the 'Tiffany Network', which alludes to the perceived high quality of CBS programming during the tenure of its founder William S. Paley. It can also refer to some of CBS's first demonstrations of color television, which were held in a former Tiffany & Co. building in New York City in 1950. The network has its origins in United Independent Broadcasters Inc., a collection of 16 radio stations that was bought by William S. Paley in 1928 and renamed the Columbia Broadcasting System. Under Paley's guidance, CBS would first become one of the largest radio networks in the United States and then one of the big three American broadcast television networks. In 1974, CBS dropped its full name and became known simply as CBS, Inc. The Westinghouse Electric Corporation acquired the network in 1995 and eventually adopted the name of the company it had bought to become CBS Corporation. In 2000, CBS came under the control of Viacom, which began as a spin-off of CBS in 1971. In late 2005, Viacom split itself and reestablished CBS Corporation with the CBS television network at its core. CBS Corporation is controlled by Sumner Redstone through National Amusements, its parent.";
		} else if(alice2.topicSet == "DIPLOMAT")
		{
			this.transform.GetComponent<UILabel>().text = "TRIESTE:" + "\n" + "\n" + "Trieste is a city and seaport in northeastern Italy. It is situated towards the end of a narrow strip of Italian territory lying between the Adriatic Sea and Slovenia, which lies almost immediately south and east of the city. Trieste is located at the head of the Gulf of Trieste and throughout history it has been influenced by its location at the crossroads of Latin, Slavic, and Germanic cultures. In 2009, it had a population of about 205,000 and it is the capital of the autonomous region Friuli Venezia Giulia and Trieste province. Trieste was one of the oldest parts of the Habsburg Monarchy. In the 19th century, it was the most important port of one of the Great Powers of Europe. As a prosperous seaport in the Mediterranean region, Trieste became the fourth largest city of the Austro-Hungarian Empire (after Vienna, Budapest, and Prague). In the fin-de-siecle period, it emerged as an important hub for literature and music. It underwent an economic revival during the 1930s, and Trieste was an important spot in the struggle between the Eastern and Western blocs after the Second World War. Today, the city is in one of the richest regions of Italy, and has been a great centre for shipping, through its port (Port of Trieste), shipbuilding and financial services. In 2012 Lonely Planet.com listed the city of Trieste as the world's most underrated travel destination.";
		} else if(alice2.topicSet == "NEWYORKER")
		{
			this.transform.GetComponent<UILabel>().text = "THE NEW YORKER:" + "\n" + "\n" + "The New Yorker is an American magazine of reportage, commentary, criticism, essays, fiction, satire, cartoons, and poetry. It is published by Condé Nast. Started as a weekly in 1925, the magazine is now published 47 times annually, with five of these issues covering two-week spans. Although its reviews and events listings often focus on the cultural life of New York City, The New Yorker has a wide audience outside of New York. It is well known for its illustrated and often topical covers, its commentaries on popular culture and eccentric Americana, its attention to modern fiction by the inclusion of short stories and literary reviews, its rigorous fact checking and copyediting, its journalism on politics and social issues, and its single-panel cartoons sprinkled throughout each issue.";
		}
	}
}
