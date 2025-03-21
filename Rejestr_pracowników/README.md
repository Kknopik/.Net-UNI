[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/EI4bqZua)
# Zadanie: Rejestr pracowników

| Termin oddania | Punkty     |
|----------------|:-----------|
| 23.11.2024 23:00    |    10      |

--- 
Przekroczenie terminu o **n** zajęć wiąże się z karą:
- punkty uzyskania za realizację zadania są dzielone przez **2<sup>n</sup>**.

--- 

Napisz program, który będzie implementacą rejestru pracowników w jednej ze światowych korporacji. 
Jako wytyczną przyjmij fakt, iż korporacja charakteryzuje się 
trzema podstawowymi typami zatrudnionych pracowników:
- **Pracownik biurowy**: charakteryzuje się następującymi cechami istotnymi dla korporacji:
    - identyfikator pracownika: unikalny dla każdego pracownika w obrębie całej korporacji
    - imię
    - nazwisko
    - wiek
    - doświadczenie
    - adres budynku w którym pracuje: adres powinien składać się z czterech elementów:
        - nazwy ulicy 
        - numeru budynku 
        - numeru lokalu 
        - nazwy miasta
    - identyfikator stanowiska biurowego: unikalny w skali całej korporacji
    - intelekt: wyrażony w iq w skali 70 - 150

- **Pracownik fizyczny**: charakteryzuje się następującymi cechami istotnymi dla korporacji:
    - identyfikator pracownika: unikalny dla każdego pracownika w obrębie całej korporacji
    - imię
    - nazwisko
    - wiek
    - doświadczenie
    - adres budynku w którym pracuje: adres powinien składać się z czterech elementów:
        - nazwy ulicy 
        - numeru budynku 
        - numeru lokalu 
        - nazwy miasta
    - siła fizyczna - wyrażona w skali od 1 - 100

- **Handlarz**: charakteryzuje się następującymi cechami istotnymi dla korporacji:
    - identyfikator pracownika: unikalny dla każdego pracownika w obrębie całej korporacji
    - imię
    - nazwisko
    - wiek
    - doświadczenie
    - adres budynku w którym pracuje: adres powinien składać się z czterech elementów:
        - nazwy ulicy 
        - numeru budynku 
        - numeru lokalu 
        - nazwy miasta
    - skuteczność: wyrażona w trzech stałych typach ``NISKA``, ``ŚREDNIA``, ``WYSOKA`` 
    - wysokość prowizji: wyrażona w procentach

Rejestr powinien umożliwić realizację następujących zadań:
- dodanie dowolnego z typów pracownika do rejestru
- usunięcie pracownika o danym identyfikatorze pracowniczym z rejestru
- dodanie kilku pracowników o różnych typach na raz do rejestru
- wyświetlenie listy wszystkich pracowników posortowanych po:
    - liczbie lat doświadczenia (malejąco),
    - wieku pracownika (rosnąco), 
    - nazwisku pracownika (zgodnie z kolejnością alfabetyczną),
    - ... rejestr powinien być gotowy do dodania innych sposobów sortowania, także kompozycji sortowań
- wyświetlenie listy pracowników, którzy pracują w mieście po nazwie podanej 
 jako parametr wejściowy
- wyświetlenie listy wszystkich pracowników wraz z ich wartością dla korporacji, 
 przy czym dla każdego z typów pracownika wartość dla korporacji obliczana jest w inny sposób:
    - dla pracownika biurowego: wartość dla korporacji obliczana jest 
    ze wzoru: ``doświadczenie * intelekt``
    - dla pracownika fizycznego: wartość dla korporacji obliczana jest 
    ze wzoru ``doświadczenie * siła / wiek``
    - dla handlowca: wartość dla korporacji obliczana jest 
    ze wzoru ``doświadczenie * skuteczność``, 
    gdzie odpowiedni typ skuteczności zamieniany jest na wartość
        - ``NISKA``: 60
        - ``ŚREDNIA``: 90
        - ``WYSOKA``: 120
    - ... system powinien być otwarty, w sensie Open-Close, na rozszerzenie o nowe typy pracowników
      i odpowiednie dla ich sposoby obliczania wartości dla korporacji.

### Uwaga 1:
- Zastanów się w jaki sposób zaimplementować obiekt pracownika w programie,
  zważywszy na to, że spora liczba cech jest wspólna dla każdego z poszczególnych typów pracownika.
- Wszystkie obiekty rejestru przechowuje w pamięci komputera w wybranej przez Ciebie kolekcji.
- Zastanów się która z kolekcji będzie najlepiej realizować zadania związane z rejestrem pracowników.

---

### Uwaga 2:
Projekt powinien również: 
- zawierać odpowiednie testy jednostkowe do implementowanej funkcjonalności
- być zgodny z SOLID
- realizować [Separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns)
- wykorzystywać koncepcję DTO oraz dziedziczenie i polimorfizm.
