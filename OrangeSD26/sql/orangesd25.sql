drop database if exists OrangeSD25 ; 
create database OrangeSD25 ; 
use OrangeSD25 ; 

create table personne (
	idpersonne int(5) not null auto_increment, 
	nom varchar(50), 
	prenom varchar(50), 
	email varchar(50), 
	mdp varchar(50), 
	primary key (idpersonne)
); 

create table commercial (
	idpersonne int(5) not null, 
	secteurAct varchar(50), 
	commission float,
	primary key(idpersonne), 
	foreign key(idpersonne) references personne (idpersonne)
	);
create table technicien (
	idpersonne int(5) not null, 
	qualification varchar(50), 
	salaire float,
	primary key(idpersonne), 
	foreign key(idpersonne) references personne (idpersonne)
	);

# procedure stockee : insetion dans les deux tables 

delimiter   $
create procedure insertCommercial (IN p_nom varchar(50), IN p_prenom varchar(50), IN p_email varchar(50), IN p_mdp varchar(50), IN p_secteurAct varchar(50), IN p_commission float )

begin 
declare p_idpersonne int ; 
insert into personne values (null, p_nom, p_prenom, p_email, p_mdp);
select idpersonne into p_idpersonne from personne where email = p_email and mdp=p_mdp; 
insert into commercial values (p_idpersonne,p_secteurAct, p_commission);

end $

delimiter  ;

# afficher la liste des commerciaux 
create view v_liste_commerciaux as (
select personne.* , commercial.secteurAct, commercial.commission 
from personne inner join commercial on personne.idpersonne = commercial.idpersonne
); 

# selectionner une personne 
delimiter  $
create procedure avoirPersonne (IN p_idpersonne int)
begin 
declare nb int ; 
select count(*) into nb from commercial where idpersonne = p_idpersonne ; 
if nb > 0 then 
select personne.* , commercial.secteurAct, commercial.commission 
from personne inner join commercial on commercial.idpersonne = personne.idpersonne and commercial.idpersonne = p_idpersonne ; 
else
select personne.* , technicien.qualification, technicien.salaire 
from personne inner join technicien on technicien.idpersonne = personne.idpersonne and technicien.idpersonne = p_idpersonne ; 
end if;

end  $
delimiter ;

#supprimer un commercial 
delimiter $ 
create procedure deleteCommercial (IN p_idpersonne int)
begin 
delete from commercial where idpersonne = p_idpersonne; 
delete from personne where idpersonne = p_idpersonne; 
end $
delimiter  ;

# modifier un commercial 

delimiter   $
create procedure updateCommercial (IN p_idpersonne int, IN p_nom varchar(50), IN p_prenom varchar(50), IN p_email varchar(50), IN p_mdp varchar(50), IN p_secteurAct varchar(50), IN p_commission float )

begin 
update personne set nom=p_nom, prenom=p_prenom, email=p_email, mdp=p_mdp where idpersonne = p_idpersonne; 
update commercial set secteurAct=p_secteurAct, commission = p_commission where idpersonne = p_idpersonne; 
end $
delimiter  ; 

#insertion dun technicien 

delimiter $
create procedure insertTechnicien(In p_nom varchar(50), In p_prenom varchar(50), In p_email varchar(50), In p_mdp varchar(50), In p_qualification varchar(50), In p_salaire float)
begin
declare p_idpersonne int;
insert into personne values(null, p_nom, p_prenom, p_email, p_mdp);
select idpersonne into p_idpersonne from personne where email = p_email and mdp = p_mdp;
insert into technicien values (p_idpersonne, p_qualification, p_salaire);
end $
delimiter ;
 

#Liste des techniciens 

create view v_liste_techniciens as (
select personne.*, technicien.qualification, technicien.salaire from personne inner join technicien on personne.idpersonne = technicien.idpersonne
);

#suppression dun technicien
 
delimiter $
create procedure deleteTechnicien (IN p_idpersonne int)
begin
delete from technicien where idpersonne = p_idpersonne;
delete from personne where idpersonne = p_idpersonne;
end $
delimiter ;

#mise à jour du technicien 
delimiter $
create procedure updateTechnicien(In p_idpersonne int, p_nom varchar(50), In p_prenom varchar(50), In p_email varchar(50), In p_mdp varchar(50), In p_qualification varchar(50), In p_salaire float)
begin
update personne set nom=p_nom, prenom=p_prenom, email=p_email, mdp=p_mdp where idpersonne = p_idpersonne;
update technicien set qualification=p_qualification, salaire=p_salaire where idpersonne = p_idpersonne;
end $
 
delimiter ;
 

#insertion de commerciaux 
call insertCommercial("JM","Heder","a@gmail.com","123", "Telecom",2); 
call insertCommercial("Aramata","Myriam","b@gmail.com","456", "Produits mobiles",5); 

#insertion des techniciens 
call insertTechnicien("Bormane","Vincent","t@gmail.com","123", "Technicien support",2000); 
call insertTechnicien("Clement","Fouad","f@gmail.com","456", "Technicien securite",2100);










