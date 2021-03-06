﻿namespace HappySkorpion.FioClient
{
    public enum PaymentReason
    {
        Reason110 = 110, // Vývoz zboží
        Reason112 = 112, // Finanční pronájem (leasing) – vývoz
        Reason120 = 120, // Dovoz zboží
        Reason122 = 122, // Finanční pronájem (leasing) – dovoz
        Reason130 = 130, // Reexport
        Reason132 = 132, // Zpracování
        Reason135 = 135, // Opravy
        Reason190 = 190, // Transakce z použití směnek a šeků
        Reason195 = 195, // Časově neidentifikované platební tituly
        Reason210 = 210, // Železniční nákladní – inkasa a platby spojené s přepravou zboží po železnici
        Reason211 = 211, // Železniční osobní – inkasa a platby spojené s přepravou osob po železnici
        Reason212 = 212, // Železniční ostatní
        Reason213 = 213, // Námořní nákladní
        Reason214 = 214, // Námořní osobní
        Reason215 = 215, // Námořní ostatní
        Reason216 = 216, // Vnitrozemská vodní nákladní
        Reason217 = 217, // Vnitrozemská vodní osobní
        Reason218 = 218, // Vnitrozemská vodní ostatní
        Reason219 = 219, // Letecká nákladní
        Reason220 = 220, // Letecká osobní
        Reason221 = 221, // Letecká ostatní
        Reason222 = 222, // Silniční nákladní
        Reason223 = 223, // Silniční osobní
        Reason224 = 224, // Silniční ostatní
        Reason226 = 226, // Kombinovaná doprava
        Reason233 = 233, // Kosmická doprava
        Reason235 = 235, // Potrubní tranzit
        Reason239 = 239, // Ostatní přepravní služby
        Reason260 = 260, // Nákup cizí měny za hotovost
        Reason262 = 262, // Nákup cizí měny s připsáním na účet fyzické osoby v Kč
        Reason265 = 265, // Nákup cizí měny s připsáním na účet právnické osoby v Kč
        Reason270 = 270, // Prodej cizí měny za hotovost
        Reason272 = 272, // Prodej cizí měny s odepsáním z účtu fyzické osoby v Kč
        Reason275 = 275, // Prodej cizí měny s odepsáním z účtu právnické osoby v Kč
        Reason280 = 280, // Aktivní cestovní ruch
        Reason282 = 282, // Pasivní cestovní ruch
        Reason285 = 285, // Mimobankovní směnárny
        Reason295 = 295, // Transakce z použití platebních karet
        Reason310 = 310, // Poštovní služby
        Reason311 = 311, // Kurýrní služby
        Reason312 = 312, // Telekomunikační a radiokomunikační služby
        Reason315 = 315, // Stavební a montážní práce v zahraničí
        Reason318 = 318, // Stavební a montážní práce v tuzemsku
        Reason320 = 320, // Ziskové operace se zbožím
        Reason325 = 325, // Opravy
        Reason326 = 326, // Pojištění zboží
        Reason327 = 327, // Zajištění (pojišťoven)
        Reason328 = 328, // Pomocné služby při pojištění
        Reason330 = 330, // Ostatní pojištění
        Reason332 = 332, // Životní a penzijní pojištění
        Reason335 = 335, // Finanční služby
        Reason340 = 340, // Reklama
        Reason345 = 345, // Právní služby
        Reason346 = 346, // Účetnické a auditorské služby
        Reason347 = 347, // Poradenství v podnikání a řízení, služby v oblasti vytváření vztahu k veřejnosti – public relations
        Reason348 = 348, // Nájemné
        Reason352 = 352, // Pronájem strojů a zařízení
        Reason355 = 355, // Výzkum a vývoj
        Reason360 = 360, // Autorské honoráře, licenční poplatky
        Reason361 = 361, // Ochranné známky, franšízy
        Reason365 = 365, // Služby výpočetní techniky
        Reason368 = 368, // Informační služby
        Reason369 = 369, // Služby mezi podniky v rámci přímých investic
        Reason370 = 370, // Diplomatická zastoupení České republiky v zahraničí
        Reason372 = 372, // Zahraniční diplomatická zastoupení v České republice
        Reason375 = 375, // Vládní příjmy a výdaje
        Reason376 = 376, // Ostatní vládní příjmy a výdaje
        Reason378 = 378, // Zprostředkovatelské služby
        Reason380 = 380, // Ostatní služby obchodní povahy
        Reason382 = 382, // Audiovizuální služby
        Reason384 = 384, // Služby v oblasti vzdělávání
        Reason385 = 385, // Služby v oblasti kultury, zábavy, sportu a rekreace
        Reason386 = 386, // Služby v oblasti zdravotnictví a veterinární péče
        Reason387 = 387, // Služby v oblasti zemědělství
        Reason388 = 388, // Služby v oblasti odpadového hospodářství
        Reason390 = 390, // Technické služby
        Reason392 = 392, // Služby v oblasti těžebního průmyslu
        Reason395 = 395, // Zastoupení českých firem v zahraničí
        Reason397 = 397, // Zastoupení zahraničních firem v ČR
        Reason410 = 410, // Převody pracovních příjmů u krátkodobého pobytu
        Reason412 = 412, // Převody pracovních příjmů u dlouhodobého pobytu
        Reason510 = 510, // Výnosy z přímých investic
        Reason520 = 520, // Výnosy z portfoliových investic
        Reason530 = 530, // Úroky – přímé investice
        Reason532 = 532, // Úroky – portfoliové investice
        Reason535 = 535, // Úroky z finančních a ostatních úvěrů
        Reason538 = 538, // Úroky z obchodních úvěrů
        Reason540 = 540, // Úroky z depozit
        Reason550 = 550, // Důchody z půdy
        Reason610 = 610, // Převody(nenávratné) – podpory, odškodnění, věna apod.
        Reason612 = 612, // Dědictví a dary
        Reason615 = 615, // Výživné
        Reason618 = 618, // Penze
        Reason620 = 620, // Příspěvky mezinárodním organizacím ze státního rozpočtu
        Reason622 = 622, // Příspěvky mezinárodním organizacím mimo státní rozpočet
        Reason625 = 625, // Převody v souvislosti s vystěhováním
        Reason628 = 628, // Zahraniční pomoc
        Reason630 = 630, // Dotace
        Reason632 = 632, // Pokuty, penále
        Reason635 = 635, // Daně a poplatky
        Reason640 = 640, // Nákup a prodej vlastnických práv a nefinančních aktiv
        Reason650 = 650, // Ostatní finanční převody
        Reason652 = 652, // Příspěvky a výhry
        Reason653 = 653, // Vklady a příspěvky do nadací a nadačních fondů
        Reason725 = 725, // Finanční deriváty
        Reason735 = 735, // Nákup a prodej nemovitostí v zahraničí
        Reason740 = 740, // Poskytnuté úvěry krátkodobé účelové
        Reason742 = 742, // Poskytnuté úvěry krátkodobé finanční (bez stanoveného účelu)
        Reason745 = 745, // Poskytnuté úvěry střednědobé a dlouhodobé účelové
        Reason748 = 748, // Poskytnuté úvěry střednědobé a dlouhodobé finanční(bez stanoveného účelu)
        Reason750 = 750, // Vklady a výběry z vkladů promptních a krátkodobých
        Reason752 = 752, // Dotace účtů
        Reason755 = 755, // Vklady a výběry z vkladů střednědobých a dlouhodobých
        Reason760 = 760, // Konverze, arbitráže a další operace
        Reason762 = 762, // Řízení likvidity peněžních prostředků(cash-pooling, zero balancing)
        Reason770 = 770, // Členské podíly v mezinárodních organizacích
        Reason790 = 790, // Zajištění závazků cizozemce
        Reason818 = 818, // Tuzemské portfoliové investice
        Reason820 = 820, // Tuzemské dluhové cenné papíry krátkodobé
        Reason822 = 822, // Tuzemské dluhové cenné papíry střednědobé a dlouhodobé
        Reason825 = 825, // Finanční deriváty
        Reason835 = 835, // Nákup a prodej nemovitostí v tuzemsku
        Reason850 = 850, // Vklady a výběry z vkladů promptních a krátkodobých
        Reason852 = 852, // Dotace účtů
        Reason855 = 855, // Vklady a výběry z vkladů střednědobých a dlouhodobých
        Reason862 = 862, // Řízení likvidity peněžních prostředků(cash-pooling, zero balancing)
        Reason890 = 890, // Zajištění závazku tuzemce
        Reason950 = 950, // Převody mezi tuzemci
        Reason952 = 952, // Převody mezi cizozemci
    }
}
