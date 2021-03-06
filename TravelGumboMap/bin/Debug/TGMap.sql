CREATE TABLE IF NOT EXISTS countries (region TEXT, country TEXT, flagid TEXT, founding_date TEXT, tgblogs TEXT);

INSERT INTO countries VALUES ("NorthAmerica", "United States", "UnitedStatesFlag.png", "July 4, 1776", "http://www.travelgumbo.com/collection/USBlogs");
INSERT INTO countries VALUES ("NorthAmerica", "Canada", "CanadaFlag.png", "July 4, 1776", "http://www.travelgumbo.com/collection/CanadaBlogs");


CREATE TABLE IF NOT EXISTS states (country TEXT, stpv TEXT, flagid TEXT, statehoodyr TEXT, tgblogs TEXT );

INSERT INTO states VALUES ("United States", "Alabama", "sflags//Alabama.gif", "December 14, 1819", "blank");
INSERT INTO states VALUES ("United States", "Alaska", "sflags//Alaska.gif", "January 3, 1959", "blank");
INSERT INTO states VALUES ("United States", "Arizona", "sflags/Arizona.gif", "February 14, 1912", "http://www.travelgumbo.com/collection/ArizonaBlogs");
INSERT INTO states VALUES ("United States", "Arkansas", "sflags//Arkansas.gif", "June 15, 1836", "blank");
INSERT INTO states VALUES ("United States", "California", "sflags//California.gif", "September 9, 1850", "http://www.travelgumbo.com/collection/CaliforniaBlogs");
INSERT INTO states VALUES ("United States", "Colorado", "sflags//Colorado.gif", "August 1, 1876", "http://www.travelgumbo.com/collection/ColoradoBlogs");
INSERT INTO states VALUES ("United States", "Connecticut", "sflags//Connecticut.gif", "January 9, 1788", "blank");
INSERT INTO states VALUES ("United States", "Delaware", "sflags//Delaware.gif", "December 7, 1787", "blank");
INSERT INTO states VALUES ("United States", "Florida", "sflags//Florida.gif", "March 3, 1845", "http://www.travelgumbo.com/collection/FloridaBlogs");
INSERT INTO states VALUES ("United States", "Georgia", "sflags//Georgia.gif", "January 2, 1788", "blank");
INSERT INTO states VALUES ("United States", "Hawaii", "sflags//Hawaii.gif", "August 21, 1959", "http://www.travelgumbo.com/collection/HawaiiBlogs");
INSERT INTO states VALUES ("United States", "Idaho", "sflags//Idaho.gif", "July 3, 1890", "http://www.travelgumbo.com/collection/IdahoBlogs");
INSERT INTO states VALUES ("United States", "Illinois", "sflags//Illinois.gif", "December 3, 1818", "http://www.travelgumbo.com/collection/IllinoisBlogs");
INSERT INTO states VALUES ("United States", "Indiana", "sflags//Indiana.gif", "December 11, 1816", "blank");
INSERT INTO states VALUES ("United States", "Iowa", "sflags//Iowa.gif", "December 28, 1846", "blank");
INSERT INTO states VALUES ("United States", "Kansas", "sflags//Kansas.gif", "January 29, 1861", "blank");
INSERT INTO states VALUES ("United States", "Kentucky", "sflags//Kentucky.gif", "June 1, 1792", "blank");
INSERT INTO states VALUES ("United States", "Louisiana", "sflags//Louisiana.gif", "April 30, 1812", "blank");
INSERT INTO states VALUES ("United States", "Maine", "sflags//Maine.gif", "March 15, 1820", "http://www.travelgumbo.com/collection/MaineBlogs");
INSERT INTO states VALUES ("United States", "Maryland", "sflags//Maryland.gif", "April 28, 1788", "blank");
INSERT INTO states VALUES ("United States", "Massachusetts", "sflags//Massachusetts.gif", "February 6, 1788", "http://www.travelgumbo.com/collection/MassachusettsBlogs");
INSERT INTO states VALUES ("United States", "Michigan", "sflags//Michigan.gif", "January 26, 1837", "blank");
INSERT INTO states VALUES ("United States", "Minnesota", "sflags//Minnesota.gif", "May 11, 1858", "http://www.travelgumbo.com/collection/MinnesotaBlogs");
INSERT INTO states VALUES ("United States", "Mississippi", "sflags//Mississippi.gif", "December 10, 1817", "blank");
INSERT INTO states VALUES ("United States", "Missouri", "sflags//Missouri.gif", "August 10, 1821", "blank");
INSERT INTO states VALUES ("United States", "Montana", "sflags//Montana.gif", "November 8, 1889", "http://www.travelgumbo.com/collection/MontanaBlogs");
INSERT INTO states VALUES ("United States", "Nebraska", "sflags//Nebraska.gif", "March 1, 1867", "blank");
INSERT INTO states VALUES ("United States", "Nevada", "sflags//Nevada.gif", "October 31, 1864", "http://www.travelgumbo.com/collection/NevadaBlogs");
INSERT INTO states VALUES ("United States", "New Hampshire", "sflags//NewHampshire.gif", "June 21, 1788", "blank");
INSERT INTO states VALUES ("United States", "New Jersey", "sflags//NewJersey.gif", "December 18, 1787", "blank");
INSERT INTO states VALUES ("United States", "New Mexico", "sflags//NewMexico.gif", "January 6, 1912", "blank");
INSERT INTO states VALUES ("United States", "New York", "sflags//NewYork.gif", "July 26, 1788", "http://www.travelgumbo.com/collection/NewYorkBlogs");
INSERT INTO states VALUES ("United States", "North Carolina", "sflags//NorthCarolina.gif", "November 21, 1789", "blank");
INSERT INTO states VALUES ("United States", "North Dakota", "sflags//NorthDakota.gif", "November 2, 1889", "blank");
INSERT INTO states VALUES ("United States", "Ohio", "sflags//Ohio.gif", "March 1, 1803", "blank");
INSERT INTO states VALUES ("United States", "Oklahoma", "sflags//Oklahoma.gif", "November 16, 1907", "blank");
INSERT INTO states VALUES ("United States", "Oregon", "sflags//Oregon.gif", "February 14, 1859", "http://www.travelgumbo.com/collection/OregonBlogs");
INSERT INTO states VALUES ("United States", "Pennsylvania", "sflags//Pennsylvania.gif", "December 12, 1787", "http://www.travelgumbo.com/collection/PennsylvaniaBlogs");
INSERT INTO states VALUES ("United States", "Rhode Island", "sflags//RhodeIsland.gif", "May 29, 1790", "http://www.travelgumbo.com/collection/RhodeIslandBlogs");
INSERT INTO states VALUES ("United States", "South Carolina", "sflags//SouthCarolina.gif", "May 23, 1788", "blank");
INSERT INTO states VALUES ("United States", "South Dakota", "sflags//SouthDakota.gif", "November 2, 1889", "http://www.travelgumbo.com/collection/SouthDakotaBlogs");
INSERT INTO states VALUES ("United States", "Tennessee", "sflags//Tennessee.gif", "June 1, 1796", "http://www.travelgumbo.com/collection/TennesseeBlogs");
INSERT INTO states VALUES ("United States", "Texas", "sflags//Texas.gif", "December 29, 1845", "blank");
INSERT INTO states VALUES ("United States", "Utah", "sflags//Utah.gif", "January 4, 1896", "http://www.travelgumbo.com/collection/UtahBlogs");
INSERT INTO states VALUES ("United States", "Vermont", "sflags//Vermont.gif", "March 4, 1791", "blank");
INSERT INTO states VALUES ("United States", "Virginia", "sflags//Virginia.gif", "June 25, 1788", "http://www.travelgumbo.com/collection/VirginiaBlogs");
INSERT INTO states VALUES ("United States", "Washington", "sflags//Washington.gif", "November 11, 1889", "http://www.travelgumbo.com/collection/WashingtonBlogs");
INSERT INTO states VALUES ("United States", "West Virginia", "sflags//WestVirginia.gif", "June 20, 1863", "blank");
INSERT INTO states VALUES ("United States", "Wisconsin", "sflags//Wisconsin.gif", "May 29, 1848", "blank");
INSERT INTO states VALUES ("United States", "Wyoming", "sflags//Wyoming.gif", "July 10, 1890", "blank");
INSERT INTO states VALUES ("Canada", "Alberta", "sflags//Alberta.gif", "September 1, 1905", "http://www.travelgumbo.com/collection/AlbertaBlogs");
INSERT INTO states VALUES ("Canada", "British Columbia", "sflags//BritishColumbia.gif", "July 20, 1871", "http://www.travelgumbo.com/collection/BritishColumbiaBlogs");
INSERT INTO states VALUES ("Canada", "Manitoba", "sflags//Manitoba.gif", "July 15, 1870", "http://www.travelgumbo.com/collection/ManitobaBlogs");
INSERT INTO states VALUES ("Canada", "New Brunswick", "sflags//NewBrunswick.gif", "July 1, 1867", "blank");
INSERT INTO states VALUES ("Canada", "Newfoundland", "sflags//Newfoundland.gif", "March 31, 1949", "blank");
INSERT INTO states VALUES ("Canada", "Nova Scotia", "sflags//NovaScotia.gif", "July 15, 1870", "http://www.travelgumbo.com/collection/NovaScotiaBlogs");
INSERT INTO states VALUES ("Canada", "NW Territories", "sflags//NorthwestTerritories.gif", "July 1, 1867", "blank");
INSERT INTO states VALUES ("Canada", "Nunavut", "sflags//Nunavut.gif", "April 1, 1999", "blank");
INSERT INTO states VALUES ("Canada", "Ontario", "sflags//Ontario.gif", "July 1, 1867", "http://www.travelgumbo.com/collection/OntarioBlogs");
INSERT INTO states VALUES ("Canada", "Prince Edward Isl", "sflags//PrinceEdwardIsland.gif", "July 1, 1873", "blank");
INSERT INTO states VALUES ("Canada", "Quebec", "sflags//Quebec.gif","July 1, 1867", "http://www.travelgumbo.com/collection/QuebecBlogs");
INSERT INTO states VALUES ("Canada", "Saskatchewan", "sflags//Saskatchewan.gif", "September 1, 1905", "blank");
INSERT INTO states VALUES ("Canada", "Yukon", "sflags//Yukon.gif", "June 13, 1898", "blank");
