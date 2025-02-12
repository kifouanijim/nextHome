Pdrop database if exists nexthome2 ; 
create database nexthome2 ; 
use nexthome2 ; 

create table agent (
	idagent int(5) not null auto_increment, 
	nom varchar(50), 
	prenom varchar(50), 
	email varchar(50), 
	mdp varchar(50), 
	primary key (idagent)
); 

create table agentvente (
	idagent int(5) not null, 
	departement varchar(50), 
	commission float,
	primary key(idagent), 
	foreign key(idagent) references agent (idagent)
	);
create table agentloc (
	idagent int(5) not null, 
	qualification varchar(50), 
	salaire float,
	primary key(idagent), 
	foreign key(idagent) references agent (idagent)
	);

# procedure stockee : insetion dans les deux tables 

delimiter   $
create procedure insertagentvente (IN p_nom varchar(50), IN p_prenom varchar(50), IN p_email varchar(50), IN p_mdp varchar(50), IN p_departement varchar(50), IN p_commission float )

begin 
declare p_idagent int ; 
insert into agent values (null, p_nom, p_prenom, p_email, p_mdp);
select idagent into p_idagent from agent where email = p_email and mdp=p_mdp; 
insert into agentvente values (p_idagent,p_departement, p_commission);

end $

delimiter  ;

# afficher la liste des agentvente
create view v_liste_agentvente as (
select agent.* , agentvente.departement, agentvente.commission 
from agent inner join agentvente on agent.idagent = agent.idagent
); 

# selectionner un agent
delimiter  $
create procedure avoiragent (IN p_idagent int)
begin 
declare nb int ; 
select count(*) into nb from agentvente where idagent = p_idagent ; 
if nb > 0 then 
select agent.* , agentvente.departement, agentvente.commission 
from agent inner join agentvente on agentvente.idagent = agent.idagent and agentvente.idagent = p_idagent ; 
else
select agent.* , agentloc.qualification, agentloc.salaire 
from agent inner join agentloc on agentloc.idagent = agent.idagent and agentloc.idagent = p_idagent ; 
end if;

end  $
delimiter ;

#supprimer un agentvente
delimiter $ 
create procedure deleteAgentvente (IN p_idagent int)
begin 
delete from agentvente where idagent = p_idagent; 
delete from agent where idagent = p_idagent; 
end $
delimiter  ;

# modifier un agentvente

delimiter   $
create procedure updateAgentvente (IN p_idagent int, IN p_nom varchar(50), IN p_prenom varchar(50), IN p_email varchar(50), IN p_mdp varchar(50), IN p_departement varchar(50), IN p_commission float )

begin 
update agent set nom=p_nom, prenom=p_prenom, email=p_email, mdp=p_mdp where idagent = p_idagent; 
update agentvente set departement=p_departement, commission = p_commission where idagent = p_idagent; 
end $
delimiter  ; 

#insertion dun agentloc 

delimiter $
create procedure insertAgentloc(In p_nom varchar(50), In p_prenom varchar(50), In p_email varchar(50), In p_mdp varchar(50), In p_qualification varchar(50), In p_salaire float)
begin
declare p_idagent int;
insert into agent values(null, p_nom, p_prenom, p_email, p_mdp);
select idagent into p_idagent from agent where email = p_email and mdp = p_mdp;
insert into agentloc values (p_idagent, p_qualification, p_salaire);
end $
delimiter ;
 

#Liste des agents de location

create view v_liste_agentloc as (
select agent.*, agentloc.qualification, agentloc.salaire from agent inner join agentloc on agent.idagent = agentloc.idagent
);

#suppression dun agent de location
 
delimiter $
create procedure deleteagentloc (IN p_idagent int)
begin
delete from agentloc where idagent = p_idagent;
delete from agent where idagent = p_idagent;
end $
delimiter ;

#mise à jour du agent de location
delimiter $
create procedure updateAgentloc(In p_idagent int, p_nom varchar(50), In p_prenom varchar(50), In p_email varchar(50), In p_mdp varchar(50), In p_qualification varchar(50), In p_salaire float)
begin
update agent set nom=p_nom, prenom=p_prenom, email=p_email, mdp=p_mdp where idagent = p_idagent;
update agentloc set qualification=p_qualification, salaire=p_salaire where idagent = p_idagent;
end $
 
delimiter ;
 

#insertion des agent de vente
call insertagentvente("JM","Heder","a@gmail.com","123", "Telecom",2); 
call insertagentvente("Aramata","Myriam","b@gmail.com","456", "Produits mobiles",5); 

#insertion des agent de location
call insertAgentloc("Bormane","Vincent","t@gmail.com","123", "Technicien support",2000); 
call insertAgentloc("Clement","Fouad","f@gmail.com","456", "Technicien securite",2100);










