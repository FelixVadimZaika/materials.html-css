let ApiPeople = {
	URL: "https://swapi.dev/api/people/",
	Next: null,
	Prev: null
}

let prev = document.getElementById("prev");
let next = document.getElementById("next");

let btnPrev = document.getElementById("prev-btn");
btnPrev.addEventListener('click', Prev);

let btnNext = document.getElementById("next-btn");
btnNext.addEventListener('click', Next);

function Prev(argument) {
	Request(ApiPeople.Prev);
}

function Next(argument) {
	Request(ApiPeople.Next);
}

function Request(url) {
	fetch(url)
		.then(response => {
			return response.json();
		})
		.then(data => {
			ApiPeople.Next = data.next;
			ApiPeople.Prev = data.previous;

			if (ApiPeople.Next == null) {
				next.setAttribute("class", "page-item disabled");
			} else {
				next.setAttribute("class", "page-item");
			}

			if (ApiPeople.Prev != null) {
				prev.setAttribute("class", "page-item");
			} else {
				prev.setAttribute("class", "page-item disabled");
			}

			return data.results;
		})
		.then(people => {
			let tbody = document.getElementById("tbody");

			while (tbody.hasChildNodes()) {
				tbody.removeChild((tbody.childNodes[0]));
			}

			for (let i = 0; i < people.length; i++) {
				let tr = document.createElement("tr");
				tr.setAttribute("scope", "row");
				tbody.appendChild(tr);

				let td1 = document.createElement("th");
				td1.innerHTML = people[i].name;
				tr.appendChild(td1);

				let td2 = document.createElement("th");
				td2.innerHTML = people[i].height;
				tr.appendChild(td2);

				let td3 = document.createElement("th");
				td3.innerHTML = people[i].mass;
				tr.appendChild(td3);

				let td4 = document.createElement("th");
				td4.innerHTML = people[i].hair_color;
				tr.appendChild(td4);

				let td5 = document.createElement("th");
				td5.innerHTML = people[i].skin_color;
				tr.appendChild(td5);

				let td6 = document.createElement("th");
				td6.innerHTML = people[i].eye_color;
				tr.appendChild(td6);

				let td7 = document.createElement("th");
				td7.innerHTML = people[i].birth_year;
				tr.appendChild(td7);

				let td8 = document.createElement("th");
				td8.innerHTML = people[i].gender;
				tr.appendChild(td8);
			}
		})
}

Request(ApiPeople.URL);