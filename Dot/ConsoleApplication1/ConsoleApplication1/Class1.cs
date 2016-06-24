{
    "Kif.Schema" : "Kif.AnswerProviderResponse[1.11]",
    "results" : [
        {
            "Kif.Schema" : "Entities.Containment.EntityResponse[2.17]",
            "Containers" : [
                {
                    "Kif.Schema" : "Entities.Containment.EntityContainer[2.18]",
                    "DataGroupContainer" : {
                        "DataGroups" : [
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 0,
                                    "Context" : 6,
                                    "Key" : "cdb:datagroupid.bullseye",
                                    "FriendlyName" : "About",
                                    "Rank" : 255,
                                    "Size" : 2,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.bullseye",
                                        "IsPlural" : true,
                                        "FriendlyName" : "About"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.image",
                                            "FriendlyName" : "Image"
                                        },
                                        "XPath" : "/EntityContent/Image",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Image"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.description",
                                            "FriendlyName" : "Description"
                                        },
                                        "XPath" : "/EntityContent/Description",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Description"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.synthetic.born",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Born"
                                        },
                                        "Name" : "Born",
                                        "Key" : "cdb:property.synthetic.born",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.born",
                                                    "FriendlyName" : "Born"
                                                },
                                                "XPath" : "/EntityContent/DateOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DateOfBirth"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.place_of_birth",
                                                    "FriendlyName" : "Birthplace"
                                                },
                                                "XPath" : "/EntityContent/PlaceOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:PlaceOfBirth"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Siblings"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Siblings",
                                        "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[1]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/0"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[2]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/1"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[3]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/2"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[4]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/3"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso/people.person.parent|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Parents"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Parents",
                                        "Key" : "mso/people.person.parent|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.parent|True",
                                                    "FriendlyName" : "Parents"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[5]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/4"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.parent|True",
                                                    "FriendlyName" : "Parents"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[6]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/5"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/films|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Movies"
                                        },
                                        "Name" : "Movies",
                                        "Key" : "rel/films|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "rel/films|True",
                                                    "FriendlyName" : "Movies"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[7]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/6"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "rel/films|True",
                                                    "FriendlyName" : "Movies"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[8]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/7"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "ext:wa.social_profile",
                                            "FriendlyName" : "Social profile"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[1]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/0"
                                    }
                                ],
                                "ReadoutText" : "Tom Cruise  is an American actor and filmmaker."
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 39,
                                    "Context" : 5,
                                    "Key" : "cdb:datagroupid.timeline",
                                    "FriendlyName" : "Timeline",
                                    "Rank" : 253,
                                    "Size" : 0,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.timeline",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Timeline"
                                    },
                                    "Visibility" : {
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:datagroupid.timeline",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Timeline"
                                        },
                                        "Name" : "Timeline",
                                        "Key" : "cdb:datagroupid.timeline",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[9]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/8/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[10]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/9/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[11]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/10/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[12]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/11/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[13]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/12/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[14]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/13/Entities.GeneralRelationship_2:RelatedEntity"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:datagroupid.timeline",
                                                    "FriendlyName" : "Timeline"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[15]/RelatedEntity",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/14/Entities.GeneralRelationship_2:RelatedEntity"
                                            }
                                        ]
                                    }
                                ],
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.ExternalQuery[2.5]",
                                    "Url" : {
                                        "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                        "Url" : "http://en.wikipedia.org/wiki/Connor_Cruise",
                                        "DisplayName" : "See more"
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 2,
                                    "Context" : 6,
                                    "Key" : "rel/sametypes|True",
                                    "FriendlyName" : "People also search for",
                                    "Rank" : 254,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "rel/sametypes|True",
                                        "IsPlural" : true,
                                        "FriendlyName" : "People also search for"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[16]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/15"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[17]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/16"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[18]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/17"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[19]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/18"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[20]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/19"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[21]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/20"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[22]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/21"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[23]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/22"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[24]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/23"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[25]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/24"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[26]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/25"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[27]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/26"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[28]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/27"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[29]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/28"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[30]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/29"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[31]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/30"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[32]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/31"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[33]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/32"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[34]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/33"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[35]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/34"
                                    }
                                ],
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.ItemCountInternalQuery[2.0]",
                                    "QueryUrlText" : "connor cruise",
                                    "QueryText" : "connor cruise",
                                    "QueryParams" : [
                                        {
                                            "Name" : "ufn",
                                            "Values" : [
                                                "connor cruise"
                                            ]
                                        },
                                        {
                                            "Name" : "sid",
                                            "Values" : [
                                                "f6a9fd2b-4d99-51cb-592d-2da51f13a67f"
                                            ]
                                        },
                                        {
                                            "Name" : "catguid",
                                            "Values" : [
                                                "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                            ]
                                        },
                                        {
                                            "Name" : "segment",
                                            "Values" : [
                                                "generic.carousel"
                                            ]
                                        }
                                    ],
                                    "ItemCount" : 20
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 46,
                                    "Context" : 5,
                                    "Key" : "cdb:datagroupid.extra_info",
                                    "FriendlyName" : "Extra Info",
                                    "Rank" : 251,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.extra_info",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Extra Info"
                                    },
                                    "Visibility" : {
                                        "IsVisible" : false
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.official_website",
                                            "FriendlyName" : "Official Website"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[3]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/2"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.wiki_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[5]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/4"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.freebase_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[2]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/1"
                                    }
                                ]
                            }
                        ],
                        "Scenario" : "GenericEntity"
                    },
                    "VisibleDataGroupCount" : 2,
                    "DebugData" : {
                        "DataVersion" : "5/15/2016 9:33 AM  2abb1979b9d6834cbf7443cc102334306278877e; Actor; aether://experiments/93f6beab-242e-494f-aebc-32b484275955; node id a78d013c; graph EntityDocuments.ss.2016-05-11_12.ss; target='SERP-XML' market='en-us' env='prod'; ELA v98.87; SatoriEntitiesMapWorkflow v99.3"
                    },
                    "MetaInformation" : {
                        "Kif.Schema" : "Entities.Common.MetaInformation[2.5]",
                        "IsTopEntity" : true,
                        "Timestamp" : 635989015837115877,
                        "TraceId" : "",
                        "MetaInfoList" : [
                            {
                                "Key" : "threshold.mobile.bullseye.mop",
                                "Value" : "0.4499"
                            },
                            {
                                "Key" : "threshold.mobile.bullseye.top",
                                "Value" : "0.45"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.mop",
                                "Value" : "1.5999"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.top",
                                "Value" : "1.6"
                            },
                            {
                                "Key" : "threshold.desktop.datagroup.expand",
                                "Value" : "0.45"
                            },
                            {
                                "Key" : "threshold.build.bullseye",
                                "Value" : "0"
                            },
                            {
                                "Key" : "threshold.build.datagroup",
                                "Value" : "1E-06"
                            },
                            {
                                "Key" : "cdb:property.synthetic.born",
                                "Value" : "15|0.507880953803103"
                            },
                            {
                                "Key" : "cdb:property.person.born",
                                "Value" : "13|0.507880953803103"
                            },
                            {
                                "Key" : "cdb:property.synthetic.lived",
                                "Value" : "10|0.507880953803103"
                            },
                            {
                                "Key" : "richnav_facebook",
                                "Value" : "9|17.8224963132727"
                            },
                            {
                                "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                "Value" : "8|11.7773611534612"
                            },
                            {
                                "Key" : "mso/people.person.parent|True",
                                "Value" : "7|5.18903818414948"
                            },
                            {
                                "Key" : "rel/films_tvshows|True",
                                "Value" : "6|0.694459669316128"
                            },
                            {
                                "Key" : "rel/films|True",
                                "Value" : "5|0.694459669316128"
                            },
                            {
                                "Key" : "rel/tvshows|True",
                                "Value" : "4|0.694459669316128"
                            },
                            {
                                "Key" : "cdb:datagroupid.timeline",
                                "Value" : "3|0.111444660768857"
                            },
                            {
                                "Key" : "rel/films.upcoming|True",
                                "Value" : "2|0.0212424852250058"
                            },
                            {
                                "Key" : "rel/sametypes|True",
                                "Value" : "1|1.1474710571455"
                            }
                        ]
                    },
                    "EntityContent" : {
                        "Kif.Schema" : "Entities.Person[2.12]",
                        "SatoriId" : "f6a9fd2b-4d99-51cb-592d-2da51f13a67f",
                        "Image" : {
                            "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                            "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNjQzMjU1OTU1OV5BMl5BanBnXkFtZTcwMjUyMjYwOA@@._V1_UY317_CR20,0,214,317_AL_.jpg",
                            "PageUrl" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                            "SourceImageProperties" : {
                                "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                "Width" : 214,
                                "Height" : 317,
                                "Format" : 1,
                                "Size" : 16518,
                                "AccentColor" : {
                                    "A" : 255,
                                    "R" : 187,
                                    "G" : 16,
                                    "B" : 31
                                }
                            },
                            "ThumbnailId" : "A199e9b9def4fad647319965006afcbd0",
                            "ThumbnailWidth" : 202,
                            "ThumbnailHeight" : 300,
                            "PartnerId" : "16.1",
                            "DisableCropping" : false,
                            "ThumbnailRenderingInfo" : {
                                "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                "ImageTypes" : {
                                    "Kif.Type" : "typedList",
                                    "Kif.ElementType" : "enum",
                                    "Kif.Value" : [
                                        1
                                    ]
                                },
                                "SuggestedCroppingParameter" : 12,
                                "SuggestedThumbnailPartnerId" : "16.2",
                                "Segment" : "Actor",
                                "IsLogo" : false
                            }
                        },
                        "Name" : "Connor Cruise",
                        "SeeMoreQuery" : {
                            "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                            "QueryUrlText" : "connor cruise",
                            "QueryText" : "connor cruise",
                            "QueryParams" : [
                                {
                                    "Name" : "ufn",
                                    "Values" : [
                                        "connor cruise"
                                    ]
                                },
                                {
                                    "Name" : "sid",
                                    "Values" : [
                                        "f6a9fd2b-4d99-51cb-592d-2da51f13a67f"
                                    ]
                                }
                            ],
                            "SnapshotExists" : true
                        },
                        "Description" : {
                            "Kif.Schema" : "Entities.Common.Description[2.8]",
                            "Text" : "Connor Cruise was born on January 17, 1995 in Florida, USA as Connor Anthony Kidman Cruise. He is an actor, known for Seven Pounds and Red Dawn.",
                            "Provider" : {
                                "Name" : "IMDb",
                                "Url" : {
                                    "Url" : "http://www.imdb.com/name/nm2982067/",
                                    "DisplayName" : "IMDb"
                                }
                            },
                            "FormattedText" : "[[RUBATO]][&Connor Cruise&] was born on January 17\\, 1995 in Florida\\, USA as Connor Anthony Kidman Cruise. He is an actor\\, known for Seven Pounds and Red Dawn."
                        },
                        "Providers" : [
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "Wikipedia",
                                "Url" : {
                                    "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                                    "DisplayName" : "Wikipedia"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "IMDb",
                                "Url" : {
                                    "Url" : "http://www.imdb.com/name/nm2982067/",
                                    "DisplayName" : "IMDb"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "Freebase",
                                "Url" : {
                                    "Url" : "http://www.freebase.com/",
                                    "DisplayName" : "Freebase"
                                }
                            }
                        ],
                        "RelatedScalars" : [
                            {
                                "Kif.Schema" : "Entities.Scalar.SocialProfileList[2.0]",
                                "SocialProfiles" : [
                                    {
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Wikipedia",
                                            "SocialNetwork" : 11
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    },
                                    {
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "IMDB",
                                            "SocialNetwork" : 10
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://www.imdb.com/name/nm2982067/"
                                        }
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Social.Twitter.TwitterProfile[2.7]",
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Twitter",
                                            "SocialNetwork" : 1
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://twitter.com/TheConnorCruise"
                                        }
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "fb:key/m/04m_vnl"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.Uri[2.7]",
                                "Url" : "http://www.imdb.com/name/nm2982067/",
                                "DisplayName" : "imdb.com"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "Connor Cruise  is an American actor and filmmaker."
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "k:wikipedia/Connor_Cruise"
                            }
                        ],
                        "RelatedEntities" : [
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "58186d13-47b6-424d-ccbc-a7b1f494a465",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA4OTA2MDgwNV5BMl5BanBnXkFtZTcwMzEwMTY4MQ@@._V1_UY317_CR8,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2490665/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12323,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 181,
                                                "G" : 32,
                                                "B" : 22
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2490665/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A87803b0789914d708923fb98e95524d4",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Suri Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "suri cruise",
                                        "QueryText" : "suri cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "suri cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "58186d13-47b6-424d-ccbc-a7b1f494a465"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_194d7397"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "03f6c983-1654-87b3-534f-c88cdc8d249f",
                                    "Name" : "Sunday Rose Kidman Urban",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "connor cruise's sister sunday rose kidman urban",
                                        "QueryText" : "connor cruise's sister sunday rose kidman urban",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "connor cruise's sister sunday rose kidman urban"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "03f6c983-1654-87b3-534f-c88cdc8d249f"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_194d7397"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ]
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "84716451-2657-c37f-c905-ae7e3d9b4bcf",
                                    "Name" : "Faith Margaret Kidman Urban",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "connor cruise's sister faith margaret kidman urban",
                                        "QueryText" : "connor cruise's sister faith margaret kidman urban",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "connor cruise's sister faith margaret kidman urban"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "84716451-2657-c37f-c905-ae7e3d9b4bcf"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_194d7397"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ]
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "a1b8f55f-cd92-cd7e-d500-aab5466cfe10",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/people/i/cbb/2007/05/05/splashnews_shuk050507a_001_cbb.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1166,
                                            "Format" : 1,
                                            "Size" : 610029,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 67,
                                                "G" : 99,
                                                "B" : 122
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "people",
                                            "Url" : {
                                                "Url" : "http://celebritybabies.people.com/2007/05/05/isabella_cruise/",
                                                "DisplayName" : "people"
                                            }
                                        },
                                        "ThumbnailId" : "Abf77104aed85b9b57eec2e9f087e1f0f",
                                        "ThumbnailWidth" : 205,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Isabella Jane Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "connor cruise's sister isabella jane cruise",
                                        "QueryText" : "connor cruise's sister isabella jane cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "connor cruise's sister isabella jane cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "a1b8f55f-cd92-cd7e-d500-aab5466cfe10"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_194d7397"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ]
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "fdc9ba0e-f198-b3c3-3cad-cf81595a654c",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM3NTU5M15BMl5BanBnXkFtZTcwMTMyMjAyMg@@._V1_UY317_CR14,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000129/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13112,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 58,
                                                "G" : 94,
                                                "B" : 145
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000129/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Acf56e67b7cc2c2cbff9de810b1c93e0c",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Tom Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "tom cruise",
                                        "QueryText" : "tom cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "tom cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_2c4b9271"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Tom Cruise is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders, his first leading role was in the romantic comedy Risky Business, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood, Cruise starred in several more successful films in the 1980s, including the dramas The Color of Money, Cocktail, Rain Man, and Born on the Fourth of July."
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "nicole kidman",
                                        "QueryText" : "nicole kidman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "nicole kidman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "162b6ca3-a565-7fa4-e407-d443009e098a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_2c4b9271"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival."
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "3afdd691-4a93-0031-63b3-d2356acb5f09",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/9/97/Red_Dawn_FilmPoster.jpeg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 259,
                                            "Height" : 383,
                                            "Format" : 1,
                                            "Size" : 25579,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 20,
                                                "G" : 17,
                                                "B" : 17
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Red_Dawn_(2012_film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "Ade077b43409f364eff00b575bc612ef0",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Red Dawn",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "red dawn 2012 movie",
                                        "QueryText" : "red dawn 2012 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "red dawn 2012"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Red Dawn is a 2012 American war film directed by Dan Bradley. The screenplay by Carl Ellsworth and Jeremy Passmore is based on the 1984 film of the same name. The film stars Chris Hemsworth, Josh Peck, Josh Hutcherson, Adrianne Palicki, Isabel Lucas, and Jeffrey Dean Morgan. The film centers on a group of young people who defend their hometown from a North Korean invasion."
                                    }
                                },
                                "Relationship" : "",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Connor Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Daryl Jenkins",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "daryl jenkins",
                                                "QueryText" : "daryl jenkins",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "daryl jenkins"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "c1b39399-05d0-b31e-ad10-bb447169b5e5"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Red Dawn."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "53b842c1-cf5e-5649-5998-d1556763dd26",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/thumb/2/2d/Seven_Pounds_poster.jpg/220px-Seven_Pounds_poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 220,
                                            "Height" : 327,
                                            "Format" : 1,
                                            "Size" : 16288,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 168,
                                                "G" : 35,
                                                "B" : 42
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Seven_Pounds",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A39c8272bcb35274666b8e76c6164e70a",
                                        "ThumbnailWidth" : 201,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Seven Pounds",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "seven pounds movie",
                                        "QueryText" : "seven pounds movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "seven pounds"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "53b842c1-cf5e-5649-5998-d1556763dd26"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Seven Pounds is a 2008 American drama film, directed by Gabriele Muccino, in which Will Smith stars as a man who sets out to change the lives of seven people. Rosario Dawson, Woody Harrelson, and Barry Pepper also star. The film was released in theaters in the United States and Canada on December 19, 2008, by Columbia Pictures. Despite generally negative reviews from critics it was a box office success, grossing $168,168,201 worldwide."
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "In 2000, Cruise returned as Ethan Hunt in the second installment of the Mission Impossible films, releasing Mission: Impossible II."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2000
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "In 2002, Cruise won the Saturn Award for Best Actor for Vanilla Sky."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2002
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "In 2006, he returned to his role as Ethan Hunt in the third installment of the Mission Impossible film series, Mission: Impossible III."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2006
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "In November 2006, Cruise and Paula Wagner announced that they had taken over the film studio United Artists."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2006
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "On November 18, 2006, Holmes and Cruise were married at the 15th-century Odescalchi Castle in Bracciano, Italy, in a Scientology ceremony attended by many Hollywood stars."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2006
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "In 2007, Cruise took a rare supporting role for the second time in Lions for Lambs, which was a commercial disappointment."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2007
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Event[2.13]",
                                    "Name" : "Wikitimeline",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Description",
                                        "Fragments" : [
                                            {
                                                "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                                "Text" : "Cruise played the central role in the historical thriller Valkyrie released on December 25, 2008 to box office success."
                                            }
                                        ]
                                    },
                                    "Time" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 2008
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "fdc9ba0e-f198-b3c3-3cad-cf81595a654c",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM3NTU5M15BMl5BanBnXkFtZTcwMTMyMjAyMg@@._V1_UY317_CR14,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000129/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13112,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 58,
                                                "G" : 94,
                                                "B" : 145
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000129/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Acf56e67b7cc2c2cbff9de810b1c93e0c",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Tom Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "tom cruise",
                                        "QueryText" : "tom cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "tom cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Tom Cruise is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders, his first leading role was in the romantic comedy Risky Business, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood, Cruise starred in several more successful films in the 1980s, including the dramas The Color of Money, Cocktail, Rain Man, and Born on the Fourth of July."
                                    }
                                },
                                "Relationship" : "Father",
                                "Description" : "Tom Cruise is Connor Cruise's father.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.father",
                                    "FriendlyName" : "Father"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "853ddca2-7889-3973-797b-004625af3a15",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/5/51/Josh_Peck_2012.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 669,
                                            "Height" : 917,
                                            "Format" : 1,
                                            "Size" : 220241,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 111,
                                                "G" : 92,
                                                "B" : 108
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://nl.wikipedia.org/wiki/Josh_Peck",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "Ab9bc433d3b57f6ab12f68111622eac45",
                                        "ThumbnailWidth" : 218,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Josh Peck",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "josh peck",
                                        "QueryText" : "josh peck",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "josh peck"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "853ddca2-7889-3973-797b-004625af3a15"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Joshua Michael \"Josh\" Peck is an American actor. He is best known for playing Josh Nichols in the Nickelodeon live-action sitcom Drake & Josh. He began his career as a child actor in the late 1990s and early 2000s, and became known to young audiences after his role on The Amanda Show. He has since acted in films such as Mean Creek, Drillbit Taylor, The Wackness, ATM, and Red Dawn, along with voicing Eddie in the Ice Age franchise. He currently voices Casey Jones in Teenage Mutant Ninja Turtles and stars as Gerald in a lead role with John Stamos in the series Grandfathered."
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Josh Peck and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Josh Peck and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "38843575-6756-bd4f-4ca2-55f6a617dc92",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://2.bp.blogspot.com/-9--xha_x0f0/TjXtZB89-SI/AAAAAAAABsQ/aYdDrd_3GLU/s1600/7786-isabel_lucas.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1200,
                                            "Format" : 1,
                                            "Size" : 274792,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 81,
                                                "G" : 109,
                                                "B" : 122
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "truffles-ruffles",
                                            "Url" : {
                                                "Url" : "http://www.truffles-ruffles.com/2011/08/underrated-beauty-isabel-lucas.html",
                                                "DisplayName" : "truffles-ruffles"
                                            }
                                        },
                                        "ThumbnailId" : "A48d3029f917b055af93d84739e359ca8",
                                        "ThumbnailWidth" : 200,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Isabel Lucas",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "isabel lucas",
                                        "QueryText" : "isabel lucas",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "isabel lucas"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "38843575-6756-bd4f-4ca2-55f6a617dc92"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Isabel Lucas and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Isabel Lucas and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "ce9d9f40-9520-4a99-57d2-4257e86dc89f",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Adrianne_Palicki_2012.jpg/210px-Adrianne_Palicki_2012.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 210,
                                            "Height" : 281,
                                            "Format" : 1,
                                            "Size" : 15643,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 192,
                                                "G" : 121,
                                                "B" : 11
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://es.wikipedia.org/wiki/Adrianne_Palicki",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A4668b01b740bbfbec99d7fb55f882f35",
                                        "ThumbnailWidth" : 210,
                                        "ThumbnailHeight" : 281,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Adrianne Palicki",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "adrianne palicki",
                                        "QueryText" : "adrianne palicki",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "adrianne palicki"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "ce9d9f40-9520-4a99-57d2-4257e86dc89f"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Adrianne Palicki and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Adrianne Palicki and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "fef60651-1c81-8f9e-d9ce-85f8a9c30353",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "https://fbcdn-profile-a.akamaihd.net/hprofile-ak-xfp1/v/t1.0-1/c0.30.200.200/p200x200/1378187_640127542697818_117763312_n.jpg?oh=e19d65d1f33b43a03ac1ab24b361fd27&oe=55793FA9&__gda__=1434470795_68d95c8dd0b3809f46b0442c4ad005f3",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 200,
                                            "Height" : 200,
                                            "Format" : 1,
                                            "Size" : 9047,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 136,
                                                "G" : 97,
                                                "B" : 67
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "facebook",
                                            "Url" : {
                                                "Url" : "https://www.facebook.com/JoshHutchersonFans",
                                                "DisplayName" : "facebook"
                                            }
                                        },
                                        "ThumbnailId" : "A5cf3133cdc5f6cb0b13fc1b86722f12a",
                                        "ThumbnailWidth" : 200,
                                        "ThumbnailHeight" : 200,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Josh Hutcherson",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "josh hutcherson",
                                        "QueryText" : "josh hutcherson",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "josh hutcherson"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "fef60651-1c81-8f9e-d9ce-85f8a9c30353"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Josh Hutcherson and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Josh Hutcherson and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "33eb9ee0-7844-0d04-572f-cc758b27b5b3",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA4NjYzNzM3M15BMl5BanBnXkFtZTcwMjI1ODc3Mg@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0388064/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 18561,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 95,
                                                "G" : 93,
                                                "B" : 110
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0388064/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A1e519834c96fe7039a92afe1f368a6c9",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Edwin Hodge",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "edwin hodge",
                                        "QueryText" : "edwin hodge",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "edwin hodge"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "33eb9ee0-7844-0d04-572f-cc758b27b5b3"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Edwin Hodge and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Edwin Hodge and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "df34449a-9c50-dd40-4872-6c75a6bab92d",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjAyNzkyMTgyOF5BMl5BanBnXkFtZTcwODI1MTkzNQ@@._V1_UY317_CR91,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0544611/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 17035,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 151,
                                                "G" : 64,
                                                "B" : 52
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0544611/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A78e46c3dbd22a811169ddf560125e00e",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "William Mapother",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "william mapother",
                                        "QueryText" : "william mapother",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "william mapother"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "df34449a-9c50-dd40-4872-6c75a6bab92d"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "f3866473-fceb-c92a-7baa-48416e9ce17a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/f/ff/Chris_Hemsworth_Thor_2_cropped.png",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 369,
                                            "Height" : 553,
                                            "Format" : 1,
                                            "Size" : 474687,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 143,
                                                "G" : 60,
                                                "B" : 60
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://tr.wikipedia.org/wiki/Chris_Hemsworth",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A97dd7b15ab1b671c36924284ebd5e8e7",
                                        "ThumbnailWidth" : 369,
                                        "ThumbnailHeight" : 553,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Chris Hemsworth",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "chris hemsworth",
                                        "QueryText" : "chris hemsworth",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "chris hemsworth"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "f3866473-fceb-c92a-7baa-48416e9ce17a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Chris Hemsworth and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Chris Hemsworth and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "0187ba40-99a3-dd1a-3e60-b2a59f82288a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BOTg4NTc1OTM3NV5BMl5BanBnXkFtZTgwNTIyMjQ3MjE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm1595801/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13567,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 161,
                                                "G" : 42,
                                                "B" : 53
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm1595801/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6e64a1a3e2b6eed86e1552d711bf4b69",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Alyssa Diaz",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "alyssa diaz",
                                        "QueryText" : "alyssa diaz",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "alyssa diaz"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "0187ba40-99a3-dd1a-3e60-b2a59f82288a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Alyssa Diaz and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Alyssa Diaz and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "e24c026d-5b75-ec53-4c8b-aa3a09b8ac87",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNDcxOTQ0MTgxNl5BMl5BanBnXkFtZTgwMzIxNDY3NTE@._V1_UY317_CR12,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0604742/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12557,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 88,
                                                "G" : 84,
                                                "B" : 88
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0604742/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A9133d7ca8a1aea6cb5d717ed700ed39e",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Jeffrey Dean Morgan",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "jeffrey dean morgan",
                                        "QueryText" : "jeffrey dean morgan",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "jeffrey dean morgan"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "e24c026d-5b75-ec53-4c8b-aa3a09b8ac87"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Jeffrey Dean Morgan and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Jeffrey Dean Morgan and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "e53f8a1c-d665-8e49-ad10-bc517c51a821",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTI0NzI0MzU1M15BMl5BanBnXkFtZTcwMjM0Mjg3MQ@@._V1_UY317_CR20,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0191442/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 11894,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 126,
                                                "G" : 77,
                                                "B" : 77
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0191442/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A2248aca0501d8a9a9677401deab1f9a6",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Brett Cullen",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "brett cullen",
                                        "QueryText" : "brett cullen",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "brett cullen"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "e53f8a1c-d665-8e49-ad10-bc517c51a821"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Peter Brett Cullen is an American actor who has appeared in numerous motion pictures and television programs. Cullen was born August 26, 1956, in Houston, Texas the son of Lucien Hugh Cullen, an oil industry executive, and Catherine Cullen. Very unexpected thing happened to Brett Cullen at the age of 16, he was surfing and got pounded by tons of waves and was hit up against rocks and other things. A man pulled him out of the water by his hair, they didn't know what to do and he laid there dead for about 20 minutes He graduated from Madison High School in Houston in 1974. Cullen graduated from the University of Houston in 1979, giving great credit to his highly acclaimed acting mentor and University of Houston professor, Cecil Pickett, who also mentored such Houston born actors as Dennis Quaid, Randy Quaid, & Brent Spiner among others. Cullen and Dennis Quaid's close friendship to this day dates back to the 1970s and it is Cullen who introduced Dennis Quaid to his current wife, Kimberly Buffington at a dinner in Austin, Texas. Cullen was recently awarded a Distinguished Alumni Award from his alma mater, the University of Houston, in April 2012."
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Brett Cullen and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Brett Cullen and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "nicole kidman",
                                        "QueryText" : "nicole kidman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "nicole kidman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "162b6ca3-a565-7fa4-e407-d443009e098a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival."
                                    }
                                },
                                "Relationship" : "Mother",
                                "Description" : "Nicole Kidman is Connor Cruise's mother.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.mother",
                                    "FriendlyName" : "Mother"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "52212889-80d8-c8ac-89cd-e15823f4208c",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://cdn.serietivu.com/wp-content/uploads/2012/11/Will-Yun-Lee1.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 500,
                                            "Height" : 500,
                                            "Format" : 1,
                                            "Size" : 79861,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 34,
                                                "G" : 29,
                                                "B" : 14
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "altervista",
                                            "Url" : {
                                                "Url" : "http://gossipscoop.altervista.org/hawaii-five-0-3-will-yun-lee-tornera-nella-terza-stagione/",
                                                "DisplayName" : "altervista"
                                            }
                                        },
                                        "ThumbnailId" : "Aedb64831483508c01490dcab84bc3d1c",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Will Yun Lee",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "will yun lee",
                                        "QueryText" : "will yun lee",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "will yun lee"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "52212889-80d8-c8ac-89cd-e15823f4208c"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Will Yun Lee and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Will Yun Lee and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "5f8fc081-a5dc-4566-eb4c-4e029089e3ab",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BODU0NTgwNzA4OF5BMl5BanBnXkFtZTcwMTExMzk5Ng@@._V1_UX214_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0158846/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 14280,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 28,
                                                "G" : 27,
                                                "B" : 43
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0158846/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Adabb0dd23e7a55018b3a123e27b71794",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Kenneth Choi",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "kenneth choi",
                                        "QueryText" : "kenneth choi",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "kenneth choi"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "5f8fc081-a5dc-4566-eb4c-4e029089e3ab"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Kenneth Choi and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Kenneth Choi and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "192ce7e7-33a5-8cac-e5e4-b7d4efb2d87f",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTc4NjQxNzM3MV5BMl5BanBnXkFtZTgwMjQyNzkyODE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0313902/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 11426,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 21,
                                                "G" : 20,
                                                "B" : 23
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0313902/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A4c88b4c96c2b82d0e07c593e53fa851a",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Matt Gerald",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "matt gerald",
                                        "QueryText" : "matt gerald",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "matt gerald"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "192ce7e7-33a5-8cac-e5e4-b7d4efb2d87f"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Matt Gerald and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Matt Gerald and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "58186d13-47b6-424d-ccbc-a7b1f494a465",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA4OTA2MDgwNV5BMl5BanBnXkFtZTcwMzEwMTY4MQ@@._V1_UY317_CR8,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2490665/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12323,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 181,
                                                "G" : 32,
                                                "B" : 22
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2490665/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A87803b0789914d708923fb98e95524d4",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Suri Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "suri cruise",
                                        "QueryText" : "suri cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "suri cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "58186d13-47b6-424d-ccbc-a7b1f494a465"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "Sister",
                                "Description" : "Suri Cruise is Connor Cruise's sister.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.sister",
                                    "FriendlyName" : "Sister"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "9890ccb7-d36b-bbd9-acda-bb7c19369bea",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://collider.com/wp-content/uploads/michael-ealy-image-2.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 1000,
                                            "Height" : 1401,
                                            "Format" : 1,
                                            "Size" : 144963,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 92,
                                                "G" : 99,
                                                "B" : 111
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "collider",
                                            "Url" : {
                                                "Url" : "http://collider.com/michael-ealy-underworld-4-awakening-interview/109780/",
                                                "DisplayName" : "collider"
                                            }
                                        },
                                        "ThumbnailId" : "A02ba8101cc6fc6c0e760ff7e691a0cde",
                                        "ThumbnailWidth" : 214,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Michael Ealy",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "michael ealy",
                                        "QueryText" : "michael ealy",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "michael ealy"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "9890ccb7-d36b-bbd9-acda-bb7c19369bea"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Michael Ealy and Connor Cruise both appear in Seven Pounds.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Michael Ealy and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Seven Pounds",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "seven pounds",
                                                "QueryText" : "seven pounds",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "seven pounds"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "53b842c1-cf5e-5649-5998-d1556763dd26"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "af437f7d-96a6-3bea-6f7e-df985ca1f8c2",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTU1NTMxODA2MV5BMl5BanBnXkFtZTcwNzQyODExOA@@._V1_UY317_CR131,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2959119/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 14830,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 138,
                                                "G" : 85,
                                                "B" : 65
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2959119/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A47486bbc2b3f7c7e7b3a56d41c9bffe5",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Julian Alcaraz",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "julian alcaraz",
                                        "QueryText" : "julian alcaraz",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "julian alcaraz"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "af437f7d-96a6-3bea-6f7e-df985ca1f8c2"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Julian Alcaraz and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Julian Alcaraz and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "9a044683-ad90-b4fb-3da3-b24bf8277e9e",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://c420561.r61.cf1.rackcdn.com/4/2392-238050.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 369,
                                            "Height" : 550,
                                            "Format" : 1,
                                            "Size" : 84702,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 103,
                                                "G" : 105,
                                                "B" : 98
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "peerie",
                                            "Url" : {
                                                "Url" : "http://peerie.com/Acting/2392/Michael-Beach/",
                                                "DisplayName" : "peerie"
                                            }
                                        },
                                        "ThumbnailId" : "Ac58b8aa879768a4e05968418dce598d6",
                                        "ThumbnailWidth" : 201,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Michael Beach",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "michael beach",
                                        "QueryText" : "michael beach",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "michael beach"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "9a044683-ad90-b4fb-3da3-b24bf8277e9e"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Michael Beach and Connor Cruise both appear in Red Dawn.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Michael Beach and Connor Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Red Dawn",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "red dawn 2012",
                                                "QueryText" : "red dawn 2012",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "red dawn 2012"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "3afdd691-4a93-0031-63b3-d2356acb5f09"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "752b961f-4be9-56a0-910b-54cb9c0d9668",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNTI2NTg0MDA3Nl5BMl5BanBnXkFtZTgwNDkwMjY2NTE@._V1_UX214_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0157312/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12538,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 34,
                                                "G" : 91,
                                                "B" : 169
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0157312/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A70ce255d1afc70dc21a43ab6f000b2bd",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Fernando Chien",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "fernando chien",
                                        "QueryText" : "fernando chien",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "fernando chien"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "752b961f-4be9-56a0-910b-54cb9c0d9668"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : ""
                            }
                        ],
                        "Gender" : 0,
                        "DateOfBirth" : {
                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                            "Year" : 1995,
                            "Month" : 1,
                            "Day" : 17
                        },
                        "Professions" : [
                            {
                                "Name" : "Actor"
                            }
                        ],
                        "PlaceOfBirth" : {
                            "Kif.Schema" : "Entities.Place[2.15]",
                            "SatoriId" : "5fece3f4-e8e8-4159-843e-f725a930ad50",
                            "Name" : "Florida, United States",
                            "SeeMoreQuery" : {
                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                "QueryUrlText" : "florida",
                                "QueryText" : "florida",
                                "QueryParams" : [
                                    {
                                        "Name" : "ufn",
                                        "Values" : [
                                            "florida"
                                        ]
                                    },
                                    {
                                        "Name" : "sid",
                                        "Values" : [
                                            "5fece3f4-e8e8-4159-843e-f725a930ad50"
                                        ]
                                    }
                                ],
                                "SnapshotExists" : true
                            },
                            "Description" : {
                                "Kif.Schema" : "Entities.Common.Description[2.8]",
                                "Text" : ""
                            }
                        },
                        "LanguagesSpoken" : [
                            {
                                "Kif.Schema" : "Entities.Language[2.10]",
                                "SatoriId" : "757135ff-b971-4bd0-ab77-8b962c22692c",
                                "Name" : "English",
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                    "QueryUrlText" : "english language",
                                    "QueryText" : "english language",
                                    "QueryParams" : [
                                        {
                                            "Name" : "ufn",
                                            "Values" : [
                                                "english language"
                                            ]
                                        },
                                        {
                                            "Name" : "sid",
                                            "Values" : [
                                                "757135ff-b971-4bd0-ab77-8b962c22692c"
                                            ]
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    "InstrumentationInfo" : [
                        {
                            "Kif.Schema" : "Entities.Instrumentation.EntityInfo[2.6]",
                            "DataSources" : [
                                "ADELE: Remove DataGroup cdb:datagroupid.readout for segment All"
                            ],
                            "ConfidenceScore" : 0
                        }
                    ],
                    "SegmentTypes" : [
                        "Actor"
                    ],
                    "SatoriTypes" : [
                        "mso:film.actor",
                        "mso:people.person",
                        "mso:biology.organism",
                        "mso:event.agent"
                    ],
                    "AppContents" : {
                        "ResultLists" : [
                        ]
                    }
                }
            ],
            "EntityScenario" : 0,
            "QContext" : {
                "Kif.Schema" : "Entities.QueryContext[2.33]",
                "DebugInfo" : [
                    "Webcaptions",
                    "SFObjectStore - SFEP.captionscombined2",
                    "EditorialIdBlackListObjectStore - Satori.GDIEditorialTriggering_BlackList",
                    "TopEntityStore - Satori.EntityPane_ID_Content.v3top; From plugin: 0",
                    "BigEntityStore - Satori.EntityPane_ID_Content.v3big; From plugin: 1",
                    "GenericEntityStore - Satori.EntityPane_ID_Content.v3big.generic; From plugin: 2",
                    "DisambigStore - Satori.EntityPane_ID_Content.v3elvtop; From plugin: 3",
                    "BigDisambigStore - Satori.EntityPane_ID_Content.v3elvbig; From plugin: 4",
                    "GenericDisambigEntityStore - Satori.EntityPane_ID_Content.v3elvbig.generic; From plugin: 5",
                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c - TopEntityStore - Satori.EntityPane_ID_Content.v3top - No SF Update - response_idx=0",
                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f - TopEntityStore - Satori.EntityPane_ID_Content.v3top - No SF Update - response_idx=0",
                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c - DisambigStore - Satori.EntityPane_ID_Content.v3elvtop - No SF Update - response_idx=3",
                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f - DisambigStore - Satori.EntityPane_ID_Content.v3elvtop - No SF Update - response_idx=3"
                ]
            },
            "DisambigContainers" : [
                {
                    "Kif.Schema" : "Entities.Containment.EntityContainer[2.18]",
                    "DataGroupContainer" : {
                        "DataGroups" : [
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 3,
                                    "Context" : 1,
                                    "Key" : "cdb:datagroupid.entityitem",
                                    "FriendlyName" : "See results for",
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.entityitem",
                                        "IsPlural" : true,
                                        "FriendlyName" : "See results for"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.image",
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/Image",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Image"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.description",
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/Description",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Description"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.synthetic.born",
                                            "IsPlural" : true,
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "Name" : "Born",
                                        "Key" : "cdb:property.synthetic.born",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.born",
                                                    "FriendlyName" : "Born"
                                                },
                                                "XPath" : "/EntityContent/DateOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DateOfBirth"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.place_of_birth",
                                                    "FriendlyName" : "Birthplace"
                                                },
                                                "XPath" : "/EntityContent/PlaceOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:PlaceOfBirth"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "Name" : "Siblings",
                                        "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[1]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/0"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[2]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/1"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[3]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/2"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                                    "FriendlyName" : "Siblings"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[4]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/3"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso/people.person.parent|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "Name" : "Parents",
                                        "Key" : "mso/people.person.parent|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.parent|True",
                                                    "FriendlyName" : "Parents"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[5]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/4"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.parent|True",
                                                    "FriendlyName" : "Parents"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[6]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/5"
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 46,
                                    "Context" : 5,
                                    "Key" : "cdb:datagroupid.extra_info",
                                    "FriendlyName" : "Extra Info",
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.extra_info",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Extra Info"
                                    },
                                    "Visibility" : {
                                        "IsVisible" : false
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.official_website",
                                            "FriendlyName" : "Official Website"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[2]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/1"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.wiki_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[3]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/2"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.freebase_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[1]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/0"
                                    }
                                ]
                            }
                        ],
                        "Scenario" : "GenericEntityListItem"
                    },
                    "DebugData" : {
                        "DataVersion" : "5/15/2016 9:38 AM  2abb1979b9d6834cbf7443cc102334306278877e; Actor; aether://experiments/93f6beab-242e-494f-aebc-32b484275955; node id f5a67915; graph EntityDocuments.ss.2016-05-11_12.ss; target='ELV-XML' market='en-us' env='prod'"
                    },
                    "MetaInformation" : {
                        "Kif.Schema" : "Entities.Common.MetaInformation[2.5]",
                        "IsTopEntity" : true,
                        "Timestamp" : 635989019387459118,
                        "TraceId" : "",
                        "MetaInfoList" : [
                            {
                                "Key" : "threshold.mobile.bullseye.mop",
                                "Value" : "0.4499"
                            },
                            {
                                "Key" : "threshold.mobile.bullseye.top",
                                "Value" : "0.45"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.mop",
                                "Value" : "1.5999"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.top",
                                "Value" : "1.6"
                            },
                            {
                                "Key" : "threshold.desktop.datagroup.expand",
                                "Value" : "0.45"
                            },
                            {
                                "Key" : "threshold.build.bullseye",
                                "Value" : "0"
                            },
                            {
                                "Key" : "threshold.build.datagroup",
                                "Value" : "1E-06"
                            },
                            {
                                "Key" : "cdb:property.synthetic.born",
                                "Value" : "15|0.507880953803103"
                            },
                            {
                                "Key" : "cdb:property.person.born",
                                "Value" : "13|0.507880953803103"
                            },
                            {
                                "Key" : "cdb:property.synthetic.lived",
                                "Value" : "10|0.507880953803103"
                            },
                            {
                                "Key" : "richnav_facebook",
                                "Value" : "9|17.8224963132727"
                            },
                            {
                                "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                "Value" : "8|11.7773611534612"
                            },
                            {
                                "Key" : "mso/people.person.parent|True",
                                "Value" : "7|5.18903818414948"
                            },
                            {
                                "Key" : "rel/films_tvshows|True",
                                "Value" : "6|0.694459669316128"
                            },
                            {
                                "Key" : "rel/films|True",
                                "Value" : "5|0.694459669316128"
                            },
                            {
                                "Key" : "rel/tvshows|True",
                                "Value" : "4|0.694459669316128"
                            },
                            {
                                "Key" : "cdb:datagroupid.timeline",
                                "Value" : "3|0.111444660768857"
                            },
                            {
                                "Key" : "rel/films.upcoming|True",
                                "Value" : "2|0.0212424852250058"
                            },
                            {
                                "Key" : "rel/sametypes|True",
                                "Value" : "1|1.1474710571455"
                            }
                        ]
                    },
                    "EntityContent" : {
                        "Kif.Schema" : "Entities.Person[2.12]",
                        "SatoriId" : "f6a9fd2b-4d99-51cb-592d-2da51f13a67f",
                        "Image" : {
                            "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                            "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNjQzMjU1OTU1OV5BMl5BanBnXkFtZTcwMjUyMjYwOA@@._V1_UY317_CR20,0,214,317_AL_.jpg",
                            "PageUrl" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                            "SourceImageProperties" : {
                                "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                "Width" : 214,
                                "Height" : 317,
                                "Format" : 1,
                                "Size" : 16518,
                                "AccentColor" : {
                                    "A" : 255,
                                    "R" : 187,
                                    "G" : 16,
                                    "B" : 31
                                }
                            },
                            "ThumbnailId" : "A199e9b9def4fad647319965006afcbd0",
                            "ThumbnailWidth" : 202,
                            "ThumbnailHeight" : 300,
                            "PartnerId" : "16.1",
                            "DisableCropping" : false,
                            "ThumbnailRenderingInfo" : {
                                "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                "ImageTypes" : {
                                    "Kif.Type" : "typedList",
                                    "Kif.ElementType" : "enum",
                                    "Kif.Value" : [
                                        1
                                    ]
                                },
                                "SuggestedCroppingParameter" : 12,
                                "SuggestedThumbnailPartnerId" : "16.2",
                                "Segment" : "Actor",
                                "IsLogo" : false
                            }
                        },
                        "Name" : "Connor Cruise",
                        "SeeMoreQuery" : {
                            "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                            "QueryUrlText" : "connor cruise",
                            "QueryText" : "connor cruise",
                            "QueryParams" : [
                                {
                                    "Name" : "ufn",
                                    "Values" : [
                                        "connor cruise"
                                    ]
                                },
                                {
                                    "Name" : "sid",
                                    "Values" : [
                                        "f6a9fd2b-4d99-51cb-592d-2da51f13a67f"
                                    ]
                                }
                            ],
                            "SnapshotExists" : true
                        },
                        "Description" : {
                            "Kif.Schema" : "Entities.Common.Description[2.8]",
                            "Text" : "Connor Cruise was born on January 17, 1995 in Florida, USA as Connor Anthony Kidman Cruise. He is an actor, known for Seven Pounds and Red Dawn.",
                            "Provider" : {
                                "Name" : "IMDb",
                                "Url" : {
                                    "Url" : "http://www.imdb.com/name/nm2982067/",
                                    "DisplayName" : "IMDb"
                                }
                            },
                            "FormattedText" : "[[RUBATO]][&Connor Cruise&] was born on January 17\\, 1995 in Florida\\, USA as Connor Anthony Kidman Cruise. He is an actor\\, known for Seven Pounds and Red Dawn."
                        },
                        "RelatedScalars" : [
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "fb:key/m/04m_vnl"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.Uri[2.7]",
                                "Url" : "http://www.imdb.com/name/nm2982067/",
                                "DisplayName" : "imdb.com"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "k:wikipedia/Connor_Cruise"
                            }
                        ],
                        "RelatedEntities" : [
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "58186d13-47b6-424d-ccbc-a7b1f494a465",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA4OTA2MDgwNV5BMl5BanBnXkFtZTcwMzEwMTY4MQ@@._V1_UY317_CR8,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2490665/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12323,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 181,
                                                "G" : 32,
                                                "B" : 22
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2490665/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A87803b0789914d708923fb98e95524d4",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Suri Cruise",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "03f6c983-1654-87b3-534f-c88cdc8d249f",
                                    "Name" : "Sunday Rose Kidman Urban",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "84716451-2657-c37f-c905-ae7e3d9b4bcf",
                                    "Name" : "Faith Margaret Kidman Urban",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "a1b8f55f-cd92-cd7e-d500-aab5466cfe10",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/people/i/cbb/2007/05/05/splashnews_shuk050507a_001_cbb.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1166,
                                            "Format" : 1,
                                            "Size" : 610029,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 67,
                                                "G" : 99,
                                                "B" : 122
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "people",
                                            "Url" : {
                                                "Url" : "http://celebritybabies.people.com/2007/05/05/isabella_cruise/",
                                                "DisplayName" : "people"
                                            }
                                        },
                                        "ThumbnailId" : "Abf77104aed85b9b57eec2e9f087e1f0f",
                                        "ThumbnailWidth" : 205,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Isabella Jane Cruise",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "fdc9ba0e-f198-b3c3-3cad-cf81595a654c",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM3NTU5M15BMl5BanBnXkFtZTcwMTMyMjAyMg@@._V1_UY317_CR14,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000129/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13112,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 58,
                                                "G" : 94,
                                                "B" : 145
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000129/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Acf56e67b7cc2c2cbff9de810b1c93e0c",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Tom Cruise",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Tom Cruise is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders, his first leading role was in the romantic comedy Risky Business, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood, Cruise starred in several more successful films in the 1980s, including the dramas The Color of Money, Cocktail, Rain Man, and Born on the Fourth of July."
                                    }
                                },
                                "Relationship" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival."
                                    }
                                },
                                "Relationship" : ""
                            }
                        ],
                        "DateOfBirth" : {
                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                            "Year" : 1995,
                            "Month" : 1,
                            "Day" : 17
                        },
                        "Professions" : [
                            {
                                "Name" : "Actor"
                            }
                        ],
                        "PlaceOfBirth" : {
                            "Kif.Schema" : "Entities.Place[2.15]",
                            "SatoriId" : "5fece3f4-e8e8-4159-843e-f725a930ad50",
                            "Name" : "Florida, United States",
                            "Description" : {
                                "Kif.Schema" : "Entities.Common.Description[2.8]",
                                "Text" : ""
                            }
                        }
                    },
                    "SegmentTypes" : [
                        "Actor"
                    ],
                    "SatoriTypes" : [
                        "mso:film.actor",
                        "mso:people.person",
                        "mso:biology.organism",
                        "mso:event.agent"
                    ],
                    "AppContents" : {
                        "ResultLists" : [
                        ]
                    }
                }
            ]
        }
    ]
}
]]></k_AnswerDataKifResponse>
<c_AnswerDataCustomQuery></c_AnswerDataCustomQuery>
<a_AnswerDataElementArray></a_AnswerDataElementArray>
<a_AnswerResultElementArray></a_AnswerResultElementArray>
<a_AnswerDataSupersetAnswerArray></a_AnswerDataSupersetAnswerArray>
<a_ExecutionPlanLabelArray></a_ExecutionPlanLabelArray></s_AnswerData>
<s_AnswerData>
<h_AnswerDataBlueprint Size = "8" > x 0000000000000000</h_AnswerDataBlueprint>
<q_AnswerDataVersion Size = "8" > 0 </ q_AnswerDataVersion >
< t_AnswerDataCreateTime Size="8">1601-01-01 00:00:00.000</t_AnswerDataCreateTime>
<d_AnswerDataFlag Size = "4" > 1 </ d_AnswerDataFlag >
< c_AnswerDataConfigLabelConstraint ></ c_AnswerDataConfigLabelConstraint >
< f_AnswerDataConfidence Size="4">0</f_AnswerDataConfidence>
<f_AnswerDataRank Size = "4" > 0.00057142856530845165 </ f_AnswerDataRank >
< f_AnswerDataRotation Size="4">0</f_AnswerDataRotation>
<d_AnswerDataGrammarID Size = "4" > 0 </ d_AnswerDataGrammarID >
< d_AnswerDataProductionID Size="4">0</d_AnswerDataProductionID>
<d_AnswerDataFeedID Size = "4" > 0 </ d_AnswerDataFeedID >
< d_AnswerDataFactID Size="4">0</d_AnswerDataFactID>
<c_AnswerDataScenario Size = "15" > EntityWebPerson </ c_AnswerDataScenario >
< c_AnswerDataFeed ></ c_AnswerDataFeed >
< c_AnswerDataUXDisplayHint Size="10">GenericKif</c_AnswerDataUXDisplayHint>
<c_AnswerDataUXDataSchema></c_AnswerDataUXDataSchema>
<c_AnswerServiceName Size = "15" > EntityWebAnswer </ c_AnswerServiceName >
< c_AnswerVirtualServiceName ></ c_AnswerVirtualServiceName >
< d_AnswerDataIDInContext Size="4">182</d_AnswerDataIDInContext>
<s_AnswerTitle Size = "70" >
  < c_AnswerElementID Size="5">Title</c_AnswerElementID>
  <c_AnswerLinkSourceURL></c_AnswerLinkSourceURL>
  <c_AnswerLinkClickURL></c_AnswerLinkClickURL>
  <c_AnswerLinkType></c_AnswerLinkType>
  <c_AnswerLinkDocType></c_AnswerLinkDocType>
  <c_AnswerLinkText></c_AnswerLinkText>
  <c_AnswerWorkflow></c_AnswerWorkflow>
  <c_AnswerAugmentations></c_AnswerAugmentations>
  <d_AnswerLinkImageWidth Size = "4" > 0 </ d_AnswerLinkImageWidth >
  < d_AnswerLinkImageHeight Size="4">0</d_AnswerLinkImageHeight>
  <i_LinkSpecificDataBag></i_LinkSpecificDataBag>
  <a_AnswerDataElementArray></a_AnswerDataElementArray>
</s_AnswerTitle>
<s_AnswerAttribution Size = "76" >
  < c_AnswerElementID Size="11">attribution</c_AnswerElementID>
  <c_AnswerLinkSourceURL></c_AnswerLinkSourceURL>
  <c_AnswerLinkClickURL></c_AnswerLinkClickURL>
  <c_AnswerLinkType></c_AnswerLinkType>
  <c_AnswerLinkDocType></c_AnswerLinkDocType>
  <c_AnswerLinkText></c_AnswerLinkText>
  <c_AnswerWorkflow></c_AnswerWorkflow>
  <c_AnswerAugmentations></c_AnswerAugmentations>
  <d_AnswerLinkImageWidth Size = "4" > 0 </ d_AnswerLinkImageWidth >
  < d_AnswerLinkImageHeight Size="4">0</d_AnswerLinkImageHeight>
  <i_LinkSpecificDataBag></i_LinkSpecificDataBag>
  <a_AnswerDataElementArray></a_AnswerDataElementArray>
</s_AnswerAttribution>
<s_AnswerRequeryEl Size = "72" >
  < c_AnswerElementID Size="7">requery</c_AnswerElementID>
  <c_AnswerLinkSourceURL></c_AnswerLinkSourceURL>
  <c_AnswerLinkClickURL></c_AnswerLinkClickURL>
  <c_AnswerLinkType></c_AnswerLinkType>
  <c_AnswerLinkDocType></c_AnswerLinkDocType>
  <c_AnswerLinkText></c_AnswerLinkText>
  <c_AnswerWorkflow></c_AnswerWorkflow>
  <c_AnswerAugmentations></c_AnswerAugmentations>
  <d_AnswerLinkImageWidth Size = "4" > 0 </ d_AnswerLinkImageWidth >
  < d_AnswerLinkImageHeight Size="4">0</d_AnswerLinkImageHeight>
  <i_LinkSpecificDataBag></i_LinkSpecificDataBag>
  <a_AnswerDataElementArray></a_AnswerDataElementArray>
</s_AnswerRequeryEl>
<c_AnswerSimpleDisplayText></c_AnswerSimpleDisplayText>
<c_AnswerDataURI></c_AnswerDataURI>
<z_AnswerDataDebugInfo></z_AnswerDataDebugInfo>
<c_AnswerDataIndicator></c_AnswerDataIndicator>
<k_AnswerDataKifResponse Size = "135390" >< ![CDATA[{
    "Kif.Schema" : "Kif.AnswerProviderResponse[1.11]",
    "results" : [
        {
            "Kif.Schema" : "Entities.Containment.EntityResponse[2.17]",
            "Containers" : [
                {
                    "Kif.Schema" : "Entities.Containment.EntityContainer[2.18]",
                    "DataGroupContainer" : {
                        "DataGroups" : [
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 0,
                                    "Context" : 6,
                                    "Key" : "cdb:datagroupid.bullseye",
                                    "FriendlyName" : "About",
                                    "Rank" : 255,
                                    "Size" : 2,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.bullseye",
                                        "IsPlural" : true,
                                        "FriendlyName" : "About"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.image",
                                            "FriendlyName" : "Image"
                                        },
                                        "XPath" : "/EntityContent/Image",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Image"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.description",
                                            "FriendlyName" : "Description"
                                        },
                                        "XPath" : "/EntityContent/Description",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Description"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.synthetic.born",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Born"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Born",
                                        "Key" : "cdb:property.synthetic.born",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.born",
                                                    "FriendlyName" : "Born"
                                                },
                                                "XPath" : "/EntityContent/DateOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DateOfBirth"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.place_of_birth",
                                                    "FriendlyName" : "Birthplace"
                                                },
                                                "XPath" : "/EntityContent/PlaceOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:PlaceOfBirth"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.person.height",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Height"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "XPath" : "/EntityContent/Height",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Height"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:people.person.networth",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Net worth"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Net worth",
                                        "Key" : "mso:people.person.networth",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso:people.person.networth",
                                                    "IsPlural" : true,
                                                    "FriendlyName" : "Net worth"
                                                },
                                                "XPath" : "/EntityContent/RelatedScalars/*[1]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/0"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.person.spouses",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Spouse"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Spouse",
                                        "Key" : "cdb:property.person.spouses",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.spouses",
                                                    "FriendlyName" : "Spouse"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[1]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/0"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.spouses",
                                                    "FriendlyName" : "Spouse"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[2]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/1"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.spouses",
                                                    "FriendlyName" : "Spouse"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[3]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/2"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb.person.domesticpartners",
                                            "FriendlyName" : "Partner"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "XPath" : "/EntityContent/RelatedEntities/*[4]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/3"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso/people.person.children|True",
                                            "IsPlural" : true,
                                            "FriendlyName" : "Children"
                                        },
                                        "PropertyDisplayHint" : 1,
                                        "Name" : "Children",
                                        "Key" : "mso/people.person.children|True",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.children|True",
                                                    "FriendlyName" : "Children"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[10]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/9"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.children|True",
                                                    "FriendlyName" : "Children"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[11]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/10"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso/people.person.children|True",
                                                    "FriendlyName" : "Children"
                                                },
                                                "XPath" : "/EntityContent/RelatedEntities/*[12]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/11"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "ext:wa.social_profile",
                                            "FriendlyName" : "Social profile"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[3]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/2"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "k:ext/EntityTypeRanking.DomType.DomType"
                                        },
                                        "XPath" : "/EntityContent/DominantType",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DominantType"
                                    }
                                ],
                                "ReadoutText" : "Tom Cruise  is an American actor and filmmaker."
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 2,
                                    "Context" : 6,
                                    "Key" : "rel/person.romance.images|True",
                                    "FriendlyName" : "Romance",
                                    "Rank" : 251,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "rel/person.romance|True",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Romance"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/person.romance|True",
                                            "FriendlyName" : "Romance"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[5]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/4"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/person.romance|True",
                                            "FriendlyName" : "Romance"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[6]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/5"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/person.romance|True",
                                            "FriendlyName" : "Romance"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[7]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/6"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/person.romance|True",
                                            "FriendlyName" : "Romance"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[8]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/7"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/person.romance|True",
                                            "FriendlyName" : "Romance"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[9]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/8"
                                    }
                                ],
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.ItemCountInternalQuery[2.0]",
                                    "QueryUrlText" : "tom cruise",
                                    "QueryText" : "tom cruise",
                                    "QueryParams" : [
                                        {
                                            "Name" : "ufn",
                                            "Values" : [
                                                "tom cruise"
                                            ]
                                        },
                                        {
                                            "Name" : "sid",
                                            "Values" : [
                                                "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                            ]
                                        },
                                        {
                                            "Name" : "catguid",
                                            "Values" : [
                                                "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                            ]
                                        },
                                        {
                                            "Name" : "segment",
                                            "Values" : [
                                                "generic.carousel"
                                            ]
                                        }
                                    ],
                                    "ItemCount" : 5
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 2,
                                    "Context" : 6,
                                    "Key" : "rel/sametypes|True",
                                    "FriendlyName" : "People also search for",
                                    "Rank" : 254,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "rel/sametypes|True",
                                        "IsPlural" : true,
                                        "FriendlyName" : "People also search for"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[33]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/32"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[34]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/33"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[35]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/34"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[36]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/35"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[37]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/36"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[38]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/37"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[39]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/38"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[40]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/39"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[41]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/40"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[42]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/41"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[43]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/42"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[44]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/43"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[45]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/44"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[46]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/45"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[47]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/46"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[48]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/47"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "rel/sametypes|True",
                                            "FriendlyName" : "People also search for"
                                        },
                                        "XPath" : "/EntityContent/RelatedEntities/*[49]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedEntities/48"
                                    }
                                ],
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.ItemCountInternalQuery[2.0]",
                                    "QueryUrlText" : "tom cruise",
                                    "QueryText" : "tom cruise",
                                    "QueryParams" : [
                                        {
                                            "Name" : "ufn",
                                            "Values" : [
                                                "tom cruise"
                                            ]
                                        },
                                        {
                                            "Name" : "sid",
                                            "Values" : [
                                                "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                            ]
                                        },
                                        {
                                            "Name" : "catguid",
                                            "Values" : [
                                                "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                            ]
                                        },
                                        {
                                            "Name" : "segment",
                                            "Values" : [
                                                "generic.carousel"
                                            ]
                                        }
                                    ],
                                    "ItemCount" : 20
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 5,
                                    "Context" : 6,
                                    "Key" : "cdb:datagroupid.exploremore",
                                    "FriendlyName" : "Explore more",
                                    "Rank" : 253,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.exploremore",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Explore more"
                                    },
                                    "Visibility" : {
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:datagroupid.exploremore",
                                            "FriendlyName" : "Explore more"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[2]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/1"
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 46,
                                    "Context" : 5,
                                    "Key" : "cdb:datagroupid.extra_info",
                                    "FriendlyName" : "Extra Info",
                                    "Rank" : 249,
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.extra_info",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Extra Info"
                                    },
                                    "Visibility" : {
                                        "IsVisible" : false
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.official_website",
                                            "FriendlyName" : "Official Website"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[5]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/4"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.wiki_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[7]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/6"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.freebase_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[4]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/3"
                                    }
                                ]
                            }
                        ],
                        "Scenario" : "GenericEntity"
                    },
                    "VisibleDataGroupCount" : 5,
                    "DebugData" : {
                        "DataVersion" : "5/15/2016 9:31 AM  2abb1979b9d6834cbf7443cc102334306278877e; Actor; aether://experiments/93f6beab-242e-494f-aebc-32b484275955; node id a78d013c; graph EntityDocuments.ss.2016-05-11_12.ss; target='SERP-XML' market='en-us' env='prod'; ELA v98.87; SatoriEntitiesMapWorkflow v99.3"
                    },
                    "MetaInformation" : {
                        "Kif.Schema" : "Entities.Common.MetaInformation[2.5]",
                        "IsTopEntity" : true,
                        "Timestamp" : 635989015027417192,
                        "TraceId" : "",
                        "MetaInfoList" : [
                            {
                                "Key" : "threshold.mobile.bullseye.mop",
                                "Value" : "0.2199"
                            },
                            {
                                "Key" : "threshold.mobile.bullseye.top",
                                "Value" : "0.22"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.mop",
                                "Value" : "0.7999"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.top",
                                "Value" : "0.8"
                            },
                            {
                                "Key" : "threshold.desktop.datagroup.expand",
                                "Value" : "0.22"
                            },
                            {
                                "Key" : "threshold.build.bullseye",
                                "Value" : "0"
                            },
                            {
                                "Key" : "threshold.build.datagroup",
                                "Value" : "1E-06"
                            },
                            {
                                "Key" : "cdb:property.synthetic.born",
                                "Value" : "34|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.person.born",
                                "Value" : "32|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.synthetic.lived",
                                "Value" : "29|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.person.height",
                                "Value" : "28|4.13490136883326"
                            },
                            {
                                "Key" : "mso:people.person.networth",
                                "Value" : "27|4.90911486967388"
                            },
                            {
                                "Key" : "cdb:property.person.spouses",
                                "Value" : "26|6.52065370074104"
                            },
                            {
                                "Key" : "cdb.person.domesticpartners",
                                "Value" : "25|6.52065370074104"
                            },
                            {
                                "Key" : "rel/person.romance|True",
                                "Value" : "24|6.52065370074104"
                            },
                            {
                                "Key" : "mso/people.person.children|True",
                                "Value" : "23|1.07074990903502"
                            },
                            {
                                "Key" : "cdb:datagroupid.experience",
                                "Value" : "22|0.00178857698896962"
                            },
                            {
                                "Key" : "rel/films_tvshows|True",
                                "Value" : "21|8.02122357316397"
                            },
                            {
                                "Key" : "rel/tvshows|True",
                                "Value" : "20|8.02122357316397"
                            },
                            {
                                "Key" : "rel/films|True",
                                "Value" : "19|8.02122357316397"
                            },
                            {
                                "Key" : "rel/person.romance.images|True",
                                "Value" : "18|6.52065370074104"
                            },
                            {
                                "Key" : "rel/films.upcoming|True",
                                "Value" : "17|0.346424286606398"
                            },
                            {
                                "Key" : "cdb:datagroupid.timeline",
                                "Value" : "16|0.261905756401386"
                            },
                            {
                                "Key" : "richnav_facebook",
                                "Value" : "15|0.205175748735764"
                            },
                            {
                                "Key" : "mso/award.nominee.award_nominations|True|mso/award.nomination.category|True",
                                "Value" : "14|0.189712109042382"
                            },
                            {
                                "Key" : "rel/artist.tracks|True",
                                "Value" : "13|0.156533097234671"
                            },
                            {
                                "Key" : "mso/people.person.parent|True",
                                "Value" : "12|0.155230053034844"
                            },
                            {
                                "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                "Value" : "11|0.147025045795492"
                            },
                            {
                                "Key" : "rel/person.awards|True",
                                "Value" : "10|0.117494816583309"
                            },
                            {
                                "Key" : "cdb:datagroupid.education",
                                "Value" : "9|0.106889617740407"
                            },
                            {
                                "Key" : "mso/people.person.education|True|mso/education.education.educational_institution|True",
                                "Value" : "8|0.106889617740407"
                            },
                            {
                                "Key" : "cdb:datagroupid.quotes",
                                "Value" : "7|0.0637114399510003"
                            },
                            {
                                "Key" : "mso/people.person.quotation|True",
                                "Value" : "6|0.0637114399510003"
                            },
                            {
                                "Key" : "mso/music.artist.album|True",
                                "Value" : "5|0.00322030190485263"
                            },
                            {
                                "Key" : "mso/organization.founder.organizations_founded|True",
                                "Value" : "4|0.00162584346921771"
                            },
                            {
                                "Key" : "mso:music.artist.active_start",
                                "Value" : "3|0.000749612250439422"
                            },
                            {
                                "Key" : "rel/sametypes|True",
                                "Value" : "2|0.746611732898386"
                            },
                            {
                                "Key" : "cdb:datagroupid.exploremore",
                                "Value" : "1|1E-05"
                            }
                        ]
                    },
                    "EntityContent" : {
                        "Kif.Schema" : "Entities.Person[2.12]",
                        "SatoriId" : "fdc9ba0e-f198-b3c3-3cad-cf81595a654c",
                        "Image" : {
                            "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                            "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM3NTU5M15BMl5BanBnXkFtZTcwMTMyMjAyMg@@._V1_UY317_CR14,0,214,317_AL_.jpg",
                            "Title" : "The thing about filmmaking is I give it everything, that's why I work so hard. I always tell young actors to take charge. It's not that hard. Sign your own checks, be responsible.",
                            "PageUrl" : "http://www.imdb.com/name/nm0000129/",
                            "SourceImageProperties" : {
                                "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                "Width" : 214,
                                "Height" : 317,
                                "Format" : 1,
                                "Size" : 13112,
                                "AccentColor" : {
                                    "A" : 255,
                                    "R" : 58,
                                    "G" : 94,
                                    "B" : 145
                                }
                            },
                            "ThumbnailId" : "Acf56e67b7cc2c2cbff9de810b1c93e0c",
                            "ThumbnailWidth" : 202,
                            "ThumbnailHeight" : 300,
                            "PartnerId" : "16.1",
                            "DisableCropping" : false,
                            "ThumbnailRenderingInfo" : {
                                "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                "ImageTypes" : {
                                    "Kif.Type" : "typedList",
                                    "Kif.ElementType" : "enum",
                                    "Kif.Value" : [
                                        1
                                    ]
                                },
                                "SuggestedCroppingParameter" : 12,
                                "SuggestedThumbnailPartnerId" : "16.2",
                                "Segment" : "Actor",
                                "IsLogo" : false
                            }
                        },
                        "Name" : "Tom Cruise",
                        "SeeMoreQuery" : {
                            "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                            "QueryUrlText" : "tom cruise",
                            "QueryText" : "tom cruise",
                            "QueryParams" : [
                                {
                                    "Name" : "ufn",
                                    "Values" : [
                                        "tom cruise"
                                    ]
                                },
                                {
                                    "Name" : "sid",
                                    "Values" : [
                                        "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                    ]
                                }
                            ],
                            "SnapshotExists" : true
                        },
                        "Description" : {
                            "Kif.Schema" : "Entities.Common.Description[2.8]",
                            "Text" : "Tom Cruise is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders, his first leading role was in the romantic comedy Risky Business, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood, Cruise starred in several more successful films in the 1980s, including the dramas The Color of Money, Cocktail, Rain Man, and Born on the Fourth of July.",
                            "SeeMoreUrl" : {
                                "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                            },
                            "Provider" : {
                                "Name" : "Wikipedia",
                                "Url" : {
                                    "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                                    "DisplayName" : "Wikipedia"
                                }
                            },
                            "FormattedText" : "[[RUBATO]][&Tom Cruise&] is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders\\, his first leading role was in the romantic comedy Risky Business\\, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood\\, Cruise starred in several more successful films in the 1980s\\, including the dramas The Color of Money\\, Cocktail\\, Rain Man\\, and Born on the Fourth of July."
                        },
                        "Providers" : [
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "Wikipedia",
                                "Url" : {
                                    "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                                    "DisplayName" : "Wikipedia"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "IMDb",
                                "Url" : {
                                    "Url" : "http://www.imdb.com/name/nm0000129/",
                                    "DisplayName" : "IMDb"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "Celebritynetworth",
                                "Url" : {
                                    "Url" : "http://www.celebritynetworth.com/richest-celebrities/actors/tom-cruise-net-worth/",
                                    "DisplayName" : "celebritynetworth.com"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                "Name" : "Freebase",
                                "Url" : {
                                    "Url" : "http://www.freebase.com/",
                                    "DisplayName" : "Freebase"
                                }
                            }
                        ],
                        "DominantType" : "American Actor",
                        "RelatedScalars" : [
                            {
                                "Kif.Schema" : "Entities.Scalar.ScalarPrice[2.6]",
                                "Date" : {
                                    "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                    "Year" : 2016
                                },
                                "Value" : 470000000,
                                "NumericType" : 2,
                                "Prefix" : 1,
                                "Unit" : 1
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.Description[2.8]",
                                "Text" : "",
                                "Fragments" : [
                                    {
                                        "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                        "Text" : "Golden Globe Award for Best Actor – Motion Picture – Drama winners",
                                        "SeeMoreQuery" : {
                                            "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                            "QueryUrlText" : "golden globe award for best actor – motion picture – drama winners",
                                            "QueryText" : "Golden Globe Award for Best Actor – Motion Picture – Drama winners",
                                            "QueryParams" : [
                                                {
                                                    "Name" : "catguid",
                                                    "Values" : [
                                                        "c780b6bb-4b84-f11b-b125-010d1f4dc17a_e6cb12411dc8340d950b4550657d0607"
                                                    ]
                                                },
                                                {
                                                    "Name" : "segment",
                                                    "Values" : [
                                                        "generic.carousel"
                                                    ]
                                                }
                                            ]
                                        }
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.SocialProfileList[2.0]",
                                "SocialProfiles" : [
                                    {
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Wikipedia",
                                            "SocialNetwork" : 11
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    },
                                    {
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "IMDB",
                                            "SocialNetwork" : 10
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://www.imdb.com/name/nm0000129/"
                                        }
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Social.Twitter.TwitterProfile[2.7]",
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Twitter",
                                            "SocialNetwork" : 1
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://twitter.com/TomCruise"
                                        }
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Social.Facebook.FacebookProfile[2.6]",
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Facebook",
                                            "SocialNetwork" : 2
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "https://www.facebook.com/officialtomcruise"
                                        },
                                        "FBid" : "",
                                        "Name" : ""
                                    },
                                    {
                                        "Site" : {
                                            "Kif.Schema" : "Entities.Social.SocialNetworkSite[2.10]",
                                            "Name" : "Tumblr",
                                            "SocialNetwork" : 14
                                        },
                                        "ProfileUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://tomcruise.tumblr.com/"
                                        }
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "fb:key/m/07r1h"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.Uri[2.7]",
                                "Url" : "http://www.tomcruise.com/",
                                "DisplayName" : "tomcruise.com"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "Tom Cruise  is an American actor and filmmaker."
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "k:wikipedia/Tom_Cruise"
                            }
                        ],
                        "RelatedEntities" : [
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "1ee20f46-a32e-d538-eaf6-95e1d45b41a6",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/instyle/images/2012/TRANSFORMATIONS/2012-katie-holmes-400.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 400,
                                            "Height" : 400,
                                            "Format" : 1,
                                            "Size" : 45633,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 146,
                                                "G" : 63,
                                                "B" : 57
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "pinterest",
                                            "Url" : {
                                                "Url" : "http://pinterest.com/pin/186336503302739206/",
                                                "DisplayName" : "pinterest"
                                            }
                                        },
                                        "ThumbnailId" : "A02d312304864ebf4fcdea77ceed584c7",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Katie Holmes",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "katie holmes",
                                        "QueryText" : "katie holmes",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "katie holmes"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "1ee20f46-a32e-d538-eaf6-95e1d45b41a6"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1978,
                                        "Month" : 12,
                                        "Day" : 18
                                    }
                                },
                                "Relationship" : "",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2006,
                                        "Month" : 11,
                                        "Day" : 18
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2012,
                                        "Month" : 8,
                                        "Day" : 20
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "nicole kidman",
                                        "QueryText" : "nicole kidman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "nicole kidman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "162b6ca3-a565-7fa4-e407-d443009e098a"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival."
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1967,
                                        "Month" : 6,
                                        "Day" : 20
                                    }
                                },
                                "Relationship" : "",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1990,
                                        "Month" : 12,
                                        "Day" : 24
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2001,
                                        "Month" : 8,
                                        "Day" : 8
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b1966cee-2daa-e277-94d9-171eec9f7045",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTYzOTkxMTc2M15BMl5BanBnXkFtZTcwODQwNTQ0NA@@._V1._SX640_SY924_.jpg",
                                        "PageUrl" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 640,
                                            "Height" : 924,
                                            "Format" : 1,
                                            "Size" : 82598,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 160,
                                                "G" : 54,
                                                "B" : 43
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Ac17e6758cd37d02310b550f0a5b99b32",
                                        "ThumbnailWidth" : 207,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mimi Rogers",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mimi rogers",
                                        "QueryText" : "mimi rogers",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mimi rogers"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b1966cee-2daa-e277-94d9-171eec9f7045"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1956,
                                        "Month" : 1,
                                        "Day" : 27
                                    }
                                },
                                "Relationship" : "",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1987,
                                        "Month" : 5,
                                        "Day" : 9
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1990,
                                        "Month" : 2,
                                        "Day" : 4
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b55e9b46-408b-526a-6098-8bcc81313c0a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/3/31/Pen%C3%A9lope_Cruz.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1163,
                                            "Format" : 1,
                                            "Size" : 390372,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 46,
                                                "G" : 14,
                                                "B" : 13
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://tr.wikipedia.org/wiki/Penélope_Cruz",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A45f697eca9355cecaed4f532c41afdbb",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Penélope Cruz",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "penélope cruz",
                                        "QueryText" : "penélope cruz",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "penélope cruz"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b55e9b46-408b-526a-6098-8bcc81313c0a"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Penélope Cruz Sánchez, known professionally as Penélope Cruz, is a Spanish actress and model. Signed by an agent at age 15, she made her acting debut at 16 on television and her feature film debut the following year in Jamón, jamón to critical acclaim. Her subsequent roles in the 1990s and 2000s included Open Your Eyes, The Hi-Lo Country, The Girl of Your Dreams and Woman on Top. Cruz achieved recognition for her lead roles in the 2001 films Vanilla Sky, All the Pretty Horses, Captain Corelli's Mandolin and Blow."
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1974,
                                        "Month" : 4,
                                        "Day" : 28
                                    }
                                },
                                "Relationship" : "",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2001,
                                        "Month" : 7
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2004,
                                        "Month" : 1
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "1ee20f46-a32e-d538-eaf6-95e1d45b41a6",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/instyle/images/2012/TRANSFORMATIONS/2012-katie-holmes-400.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 400,
                                            "Height" : 400,
                                            "Format" : 1,
                                            "Size" : 45633,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 146,
                                                "G" : 63,
                                                "B" : 57
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "pinterest",
                                            "Url" : {
                                                "Url" : "http://pinterest.com/pin/186336503302739206/",
                                                "DisplayName" : "pinterest"
                                            }
                                        },
                                        "ThumbnailId" : "A02d312304864ebf4fcdea77ceed584c7",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Katie Holmes",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "katie holmes",
                                        "QueryText" : "katie holmes",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "katie holmes"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "1ee20f46-a32e-d538-eaf6-95e1d45b41a6"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "2006 - 2012",
                                "Description" : "Katie Holmes and Tom Cruise were married for 5 years from 2006 to 2012.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2006
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2012
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b55e9b46-408b-526a-6098-8bcc81313c0a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/3/31/Pen%C3%A9lope_Cruz.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1163,
                                            "Format" : 1,
                                            "Size" : 390372,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 46,
                                                "G" : 14,
                                                "B" : 13
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://tr.wikipedia.org/wiki/Penélope_Cruz",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A45f697eca9355cecaed4f532c41afdbb",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Penélope Cruz",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "penélope cruz",
                                        "QueryText" : "penélope cruz",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "penélope cruz"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b55e9b46-408b-526a-6098-8bcc81313c0a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Penélope Cruz Sánchez, known professionally as Penélope Cruz, is a Spanish actress and model. Signed by an agent at age 15, she made her acting debut at 16 on television and her feature film debut the following year in Jamón, jamón to critical acclaim. Her subsequent roles in the 1990s and 2000s included Open Your Eyes, The Hi-Lo Country, The Girl of Your Dreams and Woman on Top. Cruz achieved recognition for her lead roles in the 2001 films Vanilla Sky, All the Pretty Horses, Captain Corelli's Mandolin and Blow.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Penélope Cruz and Tom Cruise lived together from 2001 to 2004."
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "nicole kidman",
                                        "QueryText" : "nicole kidman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "nicole kidman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "162b6ca3-a565-7fa4-e407-d443009e098a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "1990 - 2001",
                                "Description" : "Nicole Kidman and Tom Cruise were married for 10 years from 1990 to 2001.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1990
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2001
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b1966cee-2daa-e277-94d9-171eec9f7045",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTYzOTkxMTc2M15BMl5BanBnXkFtZTcwODQwNTQ0NA@@._V1._SX640_SY924_.jpg",
                                        "PageUrl" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 640,
                                            "Height" : 924,
                                            "Format" : 1,
                                            "Size" : 82598,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 160,
                                                "G" : 54,
                                                "B" : 43
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Ac17e6758cd37d02310b550f0a5b99b32",
                                        "ThumbnailWidth" : 207,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mimi Rogers",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mimi rogers",
                                        "QueryText" : "mimi rogers",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mimi rogers"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b1966cee-2daa-e277-94d9-171eec9f7045"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "1987 - 1990",
                                "Description" : "Mimi Rogers and Tom Cruise were married for 2 years from 1987 to 1990.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1987
                                    },
                                    "End" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1990
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "70c2193e-630a-dedb-a38f-cb952278abc2",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTc1NTM4NzQ1Ml5BMl5BanBnXkFtZTcwNjc3MDgwNA@@._V1_UY317_CR35,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000333/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12999,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 88,
                                                "G" : 113,
                                                "B" : 115
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000333/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A1a9d3ef532e8ea6d793c4838b6e50931",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Cher",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "cher",
                                        "QueryText" : "cher",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "cher"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "70c2193e-630a-dedb-a38f-cb952278abc2"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_942c80f0"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Cher is an American singer and actress. Described as embodying female autonomy in a male-dominated industry, she is known for her distinctive contralto singing voice and for having worked in numerous areas of entertainment, as well as adopting a variety of styles and appearances during her five-decade-long career, which has led to her being nicknamed the Goddess of Pop.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Cher and Tom Cruise dated from 1983 to 1986."
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "f6a9fd2b-4d99-51cb-592d-2da51f13a67f",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNjQzMjU1OTU1OV5BMl5BanBnXkFtZTcwMjUyMjYwOA@@._V1_UY317_CR20,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2982067/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 16518,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 187,
                                                "G" : 16,
                                                "B" : 31
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2982067/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A199e9b9def4fad647319965006afcbd0",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Connor Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "connor cruise",
                                        "QueryText" : "connor cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "connor cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_2e510106"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "Son",
                                "Description" : "Connor Cruise is Tom Cruise's son.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.son",
                                    "FriendlyName" : "Son"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "58186d13-47b6-424d-ccbc-a7b1f494a465",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA4OTA2MDgwNV5BMl5BanBnXkFtZTcwMzEwMTY4MQ@@._V1_UY317_CR8,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm2490665/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12323,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 181,
                                                "G" : 32,
                                                "B" : 22
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm2490665/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A87803b0789914d708923fb98e95524d4",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Suri Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "suri cruise",
                                        "QueryText" : "suri cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "suri cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "58186d13-47b6-424d-ccbc-a7b1f494a465"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_2e510106"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "Daughter",
                                "Description" : "Suri Cruise is Tom Cruise's daughter.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.daughter",
                                    "FriendlyName" : "Daughter"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "a1b8f55f-cd92-cd7e-d500-aab5466cfe10",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/people/i/cbb/2007/05/05/splashnews_shuk050507a_001_cbb.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 800,
                                            "Height" : 1166,
                                            "Format" : 1,
                                            "Size" : 610029,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 67,
                                                "G" : 99,
                                                "B" : 122
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "people",
                                            "Url" : {
                                                "Url" : "http://celebritybabies.people.com/2007/05/05/isabella_cruise/",
                                                "DisplayName" : "people"
                                            }
                                        },
                                        "ThumbnailId" : "Abf77104aed85b9b57eec2e9f087e1f0f",
                                        "ThumbnailWidth" : 205,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Isabella Jane Cruise",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "tom cruise's daughter isabella jane cruise",
                                        "QueryText" : "tom cruise's daughter isabella jane cruise",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "tom cruise's daughter isabella jane cruise"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "a1b8f55f-cd92-cd7e-d500-aab5466cfe10"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_2e510106"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ]
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "Daughter",
                                "Description" : "Isabella Jane Cruise is Tom Cruise's daughter.",
                                "AssociationName" : {
                                    "CanonicalKey" : "cdb:subtitle.daughter",
                                    "FriendlyName" : "Daughter"
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "69f45dcd-e8d5-7b44-c8e5-083eb2d139c8",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://borgdotcom.files.wordpress.com/2014/01/edge-of-tomorrow-2014-movie-poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 1080,
                                            "Height" : 1600,
                                            "Format" : 1,
                                            "Size" : 606035,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 188,
                                                "G" : 24,
                                                "B" : 15
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "borg",
                                            "Url" : {
                                                "Url" : "http://borg.com/2014/01/02/all-the-movies-youll-want-to-see-in-2014/",
                                                "DisplayName" : "borg"
                                            }
                                        },
                                        "ThumbnailId" : "Ae61ee631fd245b5428542fefd1714173",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Edge of Tomorrow",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "edge of tomorrow film",
                                        "QueryText" : "edge of tomorrow film",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "edge of tomorrow film"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "69f45dcd-e8d5-7b44-c8e5-083eb2d139c8"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Edge of Tomorrow is a 2014 American military science fiction film starring Tom Cruise and Emily Blunt. Doug Liman directed the film based on a screenplay adapted from the 2004 Japanese light novel All You Need Is Kill by Hiroshi Sakurazaka. The film takes place in a future where Earth is invaded by an alien race. Major William Cage, a public relations officer inexperienced in combat, is forced by his superiors to join a landing operation against the aliens. Though Cage is killed in combat, he finds himself in a time loop that sends him back to the day preceding the battle every time he dies. Cage teams up with Special Forces warrior Rita Vrataski to improve his fighting skills through the repeated days, seeking a way to defeat the extraterrestrial invaders. In late 2009, 3 Arts Productions purchased the rights to the novel and sold a spec script to the American studio Warner Bros. Pictures. The studio produced the film with the involvement of 3 Arts, the novel's publisher Viz Media, and Australian production company Village Roadshow.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2014",
                                "Description" : "Tom Cruise played Cage in Edge of Tomorrow.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2014
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "4b853fb6-8c66-47e9-8e98-812966b18131",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BYzIyYjRiMzgtYzQzMi00ZGRlLThlNTYtMWZmOGNkMGZjOWVjXkEyXkFqcGdeQXVyNjU1NTA1MTI@._V1_UY268_CR77,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt2345759/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 11532,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 97,
                                                "G" : 112,
                                                "B" : 91
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt2345759/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A08747c193c0512a8508e47a4d29200da",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "The Mummy",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "the mummy 2017 movie",
                                        "QueryText" : "the mummy 2017 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "the mummy 2017"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "4b853fb6-8c66-47e9-8e98-812966b18131"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "2017",
                                "Description" : "",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2017
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "8e996ef5-94c7-4888-99a1-5f4293ca20bc",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/thumb/f/fb/Mission_Impossible_Rogue_Nation_poster.jpg/220px-Mission_Impossible_Rogue_Nation_poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 220,
                                            "Height" : 338,
                                            "Format" : 1,
                                            "Size" : 28136,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 191,
                                                "G" : 163,
                                                "B" : 12
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Mission:_Impossible_–_Rogue_Nation",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A6a6b984b165b5b3374ef011154fe7342",
                                        "ThumbnailWidth" : 195,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mission: Impossible - Rogue Nation",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mission impossible - rogue nation 2015 movie",
                                        "QueryText" : "mission impossible - rogue nation 2015 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mission impossible - rogue nation 2015"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "8e996ef5-94c7-4888-99a1-5f4293ca20bc"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "2015",
                                "Description" : "Tom Cruise played Ethan Hunt in Mission: Impossible - Rogue Nation.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2015
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Ethan Hunt",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "ethan hunt",
                                                "QueryText" : "ethan hunt",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "ethan hunt"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "41df7362-bf6d-16dd-aa3d-c23d5f341049"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Mission: Impossible - Rogue Nation."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "5c97f164-1d68-aeee-80fa-a4c907b738bc",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/0/05/Jack_Reacher_Never_Go_Back_poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 410,
                                            "Height" : 615,
                                            "Format" : 1,
                                            "Size" : 57912,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 128,
                                                "G" : 84,
                                                "B" : 75
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Jack_Reacher:_Never_Go_Back",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A522dde4d490b00cff58bef055ade4268",
                                        "ThumbnailWidth" : 200,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Jack Reacher: Never Go Back",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "jack reacher never go back 2016",
                                        "QueryText" : "jack reacher never go back 2016",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "jack reacher never go back 2016"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "5c97f164-1d68-aeee-80fa-a4c907b738bc"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "40db06fe-7b14-ccfe-a6fd-aa469553f06e",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/thumb/2/2e/Oblivion2013Poster.jpg/220px-Oblivion2013Poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 220,
                                            "Height" : 326,
                                            "Format" : 1,
                                            "Size" : 24007,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 13,
                                                "G" : 27,
                                                "B" : 38
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Oblivion_(2013_film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A90d42fd257c696048df90984679af6d3",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Oblivion",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "oblivion 2013 movie",
                                        "QueryText" : "oblivion 2013 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "oblivion 2013"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "40db06fe-7b14-ccfe-a6fd-aa469553f06e"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Oblivion is a 2013 post-apocalyptic science fiction film based on Joseph Kosinski's unpublished graphic novel of the same name. The film was co-written, produced and directed by Kosinski. It stars Tom Cruise, Morgan Freeman, Andrea Riseborough, and Olga Kurylenko. The film was released in the U.S. on April 19, 2013. According to Kosinski, Oblivion pays homage to science fiction films of the 1970s.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2013",
                                "Description" : "Tom Cruise played Jack Harper in Oblivion.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2013
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Jack Harper",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "jack harper",
                                                "QueryText" : "jack harper",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "jack harper"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "d05f2227-3bd5-6aac-c1fa-e2e23c7e0e0a"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Oblivion."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "d60710ad-0ba0-4c34-88e7-b67653fa5f8a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://1.bp.blogspot.com/_pxjqv8VDbxc/TMUhjVsoS5I/AAAAAAAAABA/gxbYB7IK8-k/s400/top-gun-2-movie.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 200,
                                            "Height" : 200,
                                            "Format" : 1,
                                            "Size" : 16801,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 165,
                                                "G" : 104,
                                                "B" : 38
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "teaser-trailer",
                                            "Url" : {
                                                "Url" : "http://teaser-trailer.com/movie/top-gun-2/",
                                                "DisplayName" : "teaser-trailer"
                                            }
                                        },
                                        "ThumbnailId" : "Aaf50aff3ee5594d73a0e224e84cb3ba8",
                                        "ThumbnailWidth" : 200,
                                        "ThumbnailHeight" : 200,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Top Gun 2",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "top gun 2 movie",
                                        "QueryText" : "top gun 2 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "top gun 2"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "d60710ad-0ba0-4c34-88e7-b67653fa5f8a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : ""
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "7a11ace9-2d14-d7f0-c1b6-41d9f4a67dc8",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/d/d1/Jack_Reacher_poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 295,
                                            "Height" : 437,
                                            "Format" : 1,
                                            "Size" : 59829,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 54,
                                                "G" : 63,
                                                "B" : 107
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Jack_Reacher_(film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A1c943f7492ec4201541b9c306cf7ea94",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Jack Reacher",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "jack reacher 2012 movie",
                                        "QueryText" : "jack reacher 2012 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "jack reacher 2012"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "7a11ace9-2d14-d7f0-c1b6-41d9f4a67dc8"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Jack Reacher is a 2012 American action thriller film. It is an adaptation of Lee Child's 2005 novel One Shot. Written and directed by Christopher McQuarrie, the film stars Tom Cruise as Jack Reacher and Rosamund Pike as attorney Helen Rodin. Also featured are actors David Oyelowo, Richard Jenkins, Jai Courtney, Werner Herzog, and Robert Duvall. The film entered production in October 2011, and concluded in January 2012. It was filmed entirely on location in Pittsburgh, Pennsylvania. It received generally positive reviews and performed serviceably at the North American box office.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2012",
                                "Description" : "Tom Cruise played Jack Reacher in Jack Reacher.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2012
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Jack Reacher",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "jack reacher series",
                                                "QueryText" : "jack reacher series",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "jack reacher series"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "10e50627-24c4-c1ac-2cbd-f3d6c293d050"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Jack Reacher."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "693f9bdc-21a9-98bb-5f70-9e0556ad047d",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/4/46/Top_Gun_Movie.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 240,
                                            "Height" : 350,
                                            "Format" : 1,
                                            "Size" : 22635,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 65,
                                                "G" : 91,
                                                "B" : 138
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Top_Gun",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A299bd58842a065ad1e2a9d967d365249",
                                        "ThumbnailWidth" : 205,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Top Gun",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "top gun 1986 movie",
                                        "QueryText" : "top gun 1986 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "top gun 1986"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "693f9bdc-21a9-98bb-5f70-9e0556ad047d"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Top Gun is a 1986 American action drama film directed by Tony Scott, and produced by Don Simpson and Jerry Bruckheimer, in association with Paramount Pictures. The screenplay was written by Jim Cash and Jack Epps, Jr., and was inspired by an article titled \"Top Guns\" published in California magazine three years earlier.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "1986",
                                "Description" : "Tom Cruise played Maverick in Top Gun.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1986
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Maverick",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "pete mitchell top gun",
                                                "QueryText" : "pete mitchell top gun",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "pete mitchell top gun"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "9122ef53-af74-45cf-9489-9ec60c678030"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Top Gun."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "ce845f76-f1b1-a82d-5079-766eada75214",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://cps-static.rovicorp.com/2/Open/CinemaSource/Rock of Ages/Italian/_2by3/_derived_jpg_q90_584x800_m0/113759R1.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 533,
                                            "Height" : 800,
                                            "Format" : 1,
                                            "Size" : 157828,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 21,
                                                "G" : 18,
                                                "B" : 44
                                            }
                                        },
                                        "ThumbnailId" : "A8619694c18d61314ad363df36bdfa8e4",
                                        "ThumbnailWidth" : 199,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Rock of Ages",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "rock of ages movie",
                                        "QueryText" : "rock of ages movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "rock of ages movie"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "ce845f76-f1b1-a82d-5079-766eada75214"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Rock of Ages is a 2012 American romantic musical comedy film directed by Adam Shankman. The film is an adaptation of the 2006 rock jukebox Broadway musical of the same name by Chris D'Arienzo. Originally scheduled to enter production in summer 2009 for a 2011 release, it eventually started production in May 2011 and was released on June 15, 2012.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2012",
                                "Description" : "Tom Cruise played Stacee Jaxx in Rock of Ages.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2012
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Stacee Jaxx",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "stacey jax",
                                                "QueryText" : "stacey jax",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "stacey jax"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "2217643f-b12d-f5fb-7778-0d1e36d8230f"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Rock of Ages."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "86f46003-c9a3-33fe-3325-440a080ed23d",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTY4MTUxMjQ5OV5BMl5BanBnXkFtZTcwNTUyMzg5Ng@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt1229238/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 14096,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 192,
                                                "G" : 13,
                                                "B" : 11
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt1229238/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A222c4e2b6feffbbe57a32a7f225c3b14",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mission: Impossible - Ghost Protocol",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mission impossible - ghost protocol 2011 movie",
                                        "QueryText" : "mission impossible - ghost protocol 2011 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mission impossible - ghost protocol 2011"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "86f46003-c9a3-33fe-3325-440a080ed23d"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Mission: Impossible – Ghost Protocol is a 2011 American action spy film and the fourth installment in the Mission: Impossible film series, and director Brad Bird's first live-action film. It stars Tom Cruise, who reprises his role of IMF agent Ethan Hunt, with Jeremy Renner, Simon Pegg, and Paula Patton as his supporting team. Ghost Protocol was written by André Nemec and Josh Appelbaum, and produced by Cruise, J. J. Abrams and Bryan Burk. It saw the return of editor Paul Hirsch and visual effects supervisor John Knoll from the first film, and is also the first Mission: Impossible film to be partially filmed using IMAX cameras. Released in North America by Paramount Pictures on December 16, 2011, the film was a critical and commercial success. Ghost Protocol became the highest-grossing film in the series, and the highest-grossing film starring Cruise.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2011",
                                "Description" : "Tom Cruise played Ethan Hunt in Mission: Impossible - Ghost Protocol.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2011
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Ethan Hunt",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "ethan hunt",
                                                "QueryText" : "ethan hunt",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "ethan hunt"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "41df7362-bf6d-16dd-aa3d-c23d5f341049"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Mission: Impossible - Ghost Protocol."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "990c900c-6c74-863a-170e-1e3e3b18f09e",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://cps-static.rovicorp.com/2/Open/20th_Century_Fox_39/Program/12150059/12150059_KnightAndDay_PA.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 1400,
                                            "Height" : 2100,
                                            "Format" : 1,
                                            "Size" : 1062600,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 183,
                                                "G" : 20,
                                                "B" : 36
                                            }
                                        },
                                        "ThumbnailId" : "Af7cb78463dd5700ed76c0fe1ac744f0d",
                                        "ThumbnailWidth" : 200,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Knight and Day",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "knight and day 2010 movie",
                                        "QueryText" : "knight and day 2010 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "knight and day 2010"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "990c900c-6c74-863a-170e-1e3e3b18f09e"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Knight and Day is a 2010 American action spy romantic comedy film starring Tom Cruise and Cameron Diaz. The film, directed by James Mangold, is Cruise and Diaz's second on-screen collaboration following the 2001 film Vanilla Sky. Diaz plays June Havens, a classic car restorer who unwittingly gets caught up with the eccentric secret agent Roy Miller, played by Cruise, who is on the run from the CIA.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2010",
                                "Description" : "Tom Cruise played Roy Miller in Knight and Day.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2010
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Roy Miller",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "roy miller",
                                                "QueryText" : "roy miller",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "roy miller"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "245875c8-0452-b498-31cd-3b9d520a4d61"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Knight and Day."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "a13555fd-b002-ca19-97fd-d83f602bc074",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/4/45/Collateral_%28Movie%29.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 290,
                                            "Height" : 430,
                                            "Format" : 1,
                                            "Size" : 35594,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 149,
                                                "G" : 103,
                                                "B" : 54
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Collateral_(film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A3f9e43de576f1c4c1ab7a94edade8d6d",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Collateral",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "collateral 2004 movie",
                                        "QueryText" : "collateral 2004 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "collateral 2004"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "a13555fd-b002-ca19-97fd-d83f602bc074"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Collateral is a 2004 American neo-noir crime thriller directed by Michael Mann and written by Stuart Beattie. It stars Tom Cruise cast against type as a contract killer and Jamie Foxx as a taxi driver who finds himself his hostage during an evening of the hitman's work. The film also features Jada Pinkett Smith and Mark Ruffalo. Foxx and Cruise's performances were widely praised, with Foxx being nominated for the Academy Award for Best Supporting Actor.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2004",
                                "Description" : "Tom Cruise played Vincent in Collateral.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2004
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Vincent",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "vincent collateral",
                                                "QueryText" : "vincent collateral",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "vincent collateral"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "250ded23-e1ac-6811-3a1c-7b6efb088ff2"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Collateral."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "5d858373-b54d-1c9f-f465-bdce75cd17a0",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTkxNjc2NjQwOF5BMl5BanBnXkFtZTcwMDE2NDU2MQ@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt0116695/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 13053,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 151,
                                                "G" : 93,
                                                "B" : 52
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt0116695/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A48fcf82568594948f01dc7e8088c843d",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Jerry Maguire",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "jerry maguire movie",
                                        "QueryText" : "jerry maguire movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "jerry maguire"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "5d858373-b54d-1c9f-f465-bdce75cd17a0"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Jerry Maguire is a 1996 American romantic comedy-drama sports film written, produced and directed by Cameron Crowe, and stars Tom Cruise, Cuba Gooding, Jr. and Renée Zellweger. Produced in part by long time Simpsons producer James L. Brooks. It was inspired by sports agent Leigh Steinberg, who acted as Technical Consultant on the crew. It was released in North American theaters on December 13, 1996, produced by Gracie Films and distributed by TriStar Pictures.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "1996",
                                "Description" : "Tom Cruise played Jerry Maguire in Jerry Maguire.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1996
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "e1212734-5d2c-66ab-2c43-d9c0904fda75",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTg3Njc2ODEyN15BMl5BanBnXkFtZTcwNTAwMzc3NA@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt0985699/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 14193,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 184,
                                                "G" : 5,
                                                "B" : 6
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt0985699/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a7ab209c56f1930faf1f6466666c71f",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Valkyrie",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "valkyrie 2008 movie",
                                        "QueryText" : "valkyrie 2008 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "valkyrie 2008"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "e1212734-5d2c-66ab-2c43-d9c0904fda75"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Valkyrie is a 2008 American-German historical thriller film set in Nazi Germany during World War II. The film depicts the 20 July plot in 1944 by German army officers to assassinate Adolf Hitler and to use the Operation Valkyrie national emergency plan to take control of the country. Valkyrie was directed by Bryan Singer for the American studio United Artists, and the film stars Tom Cruise as Colonel Claus von Stauffenberg, one of the key plotters. The cast included Kenneth Branagh, Bill Nighy, Eddie Izzard, Terence Stamp and Tom Wilkinson.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2008",
                                "Description" : "Tom Cruise played Colonel Claus von Stauffenberg in Valkyrie.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2008
                                    }
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "498ecc44-3136-99fd-0fbd-10385a4a87b3",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTg1NDc4NDkzNF5BMl5BanBnXkFtZTYwMTQyNTg4._V1_UY268_CR8,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt0086200/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 12513,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 188,
                                                "G" : 111,
                                                "B" : 15
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt0086200/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6e74aea1e1a4a93df9a2446edff42750",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Risky Business",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "risky business 1983 movie",
                                        "QueryText" : "risky business 1983 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "risky business 1983"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "498ecc44-3136-99fd-0fbd-10385a4a87b3"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Risky Business is a 1983 American romantic comedy film written and directed by Paul Brickman, making his directorial debut. It stars Tom Cruise and Rebecca De Mornay. The film launched Cruise to stardom. It covers themes including materialism, loss of innocence, coming of age and capitalism.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "1983",
                                "Description" : "Tom Cruise played Joel Goodsen in Risky Business.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1983
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Joel Goodsen",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "joel goodsen",
                                                "QueryText" : "joel goodsen",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "joel goodsen"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "cc256f2b-f7b8-398d-cc7e-1d0232739933"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Risky Business."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "db6a79c7-331f-6fbe-18f7-34828859b1a3",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTc1NDI5NzQyNF5BMl5BanBnXkFtZTYwMjc4NTE5._V1_UX182_CR0,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt0181689/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 11168,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 56,
                                                "G" : 78,
                                                "B" : 91
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt0181689/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A5d2b617e4e9fe27ceaa74a315ada4268",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Minority Report",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "minority report 2002 movie",
                                        "QueryText" : "minority report 2002 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "minority report 2002"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "db6a79c7-331f-6fbe-18f7-34828859b1a3"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Minority Report is a 2002 American neo-noir action mystery-thriller film directed by Steven Spielberg and loosely based on the short story of the same name by Philip K. Dick. It is set primarily in Washington, D.C., and Northern Virginia in the year 2054, where \"PreCrime\", a specialized police department, apprehends criminals based on foreknowledge provided by three psychics called \"precogs\". The cast includes Tom Cruise as Chief of PreCrime John Anderton, Colin Farrell as Department of Justice agent Danny Witwer, Samantha Morton as the senior precog Agatha, and Max von Sydow as Anderton's superior Lamar Burgess. The film is a combination of whodunit, thriller and science fiction. It is also a traditional chase film, as the main protagonist is accused of a crime he has not committed and becomes a fugitive.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2002",
                                "Description" : "Tom Cruise played Chief John Anderton in Minority Report.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2002
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Chief John Anderton",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "chief john anderton",
                                                "QueryText" : "chief john anderton",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "chief john anderton"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "60d967bc-ba36-5fca-f6d4-a0d6c0c35e48"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Minority Report."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "dacab182-102a-d9ea-5941-54c705ea349a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/e/e1/MissionImpossiblePoster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 298,
                                            "Height" : 442,
                                            "Format" : 1,
                                            "Size" : 25835,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 165,
                                                "G" : 38,
                                                "B" : 50
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Mission:_Impossible_(film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A33175f710844f7438d7f60b284954688",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mission: Impossible",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mission impossible 1996 movie",
                                        "QueryText" : "mission impossible 1996 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mission impossible 1996"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "dacab182-102a-d9ea-5941-54c705ea349a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Mission: Impossible is a 1996 American action spy film directed by Brian De Palma, produced by and starring Tom Cruise. Based on the television series of the same name, the plot follows Ethan Hunt and his mission to uncover the mole who has framed him for the murders of his entire IMF team. Work on the script had begun early with filmmaker Sydney Pollack on board, before De Palma, Steven Zaillian, David Koepp, and Robert Towne were brought in. Mission: Impossible went into pre-production without a shooting script. De Palma came up with some action sequences, but Koepp and Towne were dissatisfied with the story that led up to those events.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "1996",
                                "Description" : "Tom Cruise played Ethan Hunt in Mission: Impossible.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 1996
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Ethan Hunt",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "ethan hunt",
                                                "QueryText" : "ethan hunt",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "ethan hunt"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "41df7362-bf6d-16dd-aa3d-c23d5f341049"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Mission: Impossible."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "64e9ccdd-a607-79e1-44c3-76a2eb64a4d4",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/9/9b/Vanilla_sky_post.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 288,
                                            "Height" : 426,
                                            "Format" : 1,
                                            "Size" : 33356,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 42,
                                                "G" : 123,
                                                "B" : 161
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Vanilla_Sky",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A9edd63a89db0160f4fd4f90f203cabe3",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Vanilla Sky",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "vanilla sky movie",
                                        "QueryText" : "vanilla sky movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "vanilla sky"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "64e9ccdd-a607-79e1-44c3-76a2eb64a4d4"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Vanilla Sky is a 2001 American science fiction psychological thriller film directed, written, and co-produced by Cameron Crowe. It is an English-language remake of Alejandro Amenábar's 1997 Spanish film Open Your Eyes, which was written by Amenábar and Mateo Gil, with Penélope Cruz reprising her role from the original film. The film has been described as \"an odd mixture of science fiction, romance and reality warp\".",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2001",
                                "Description" : "Tom Cruise played David Aames in Vanilla Sky.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2001
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "David Aames",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "david aames",
                                                "QueryText" : "david aames",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "david aames"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "d77c7a7c-b3eb-f59c-f6cb-cd06f6b138ca"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Vanilla Sky."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "6fcc63d9-0c21-cd9f-d0f6-70d95819746b",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/en/8/83/War_of_the_Worlds_2005_poster.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 236,
                                            "Height" : 350,
                                            "Format" : 1,
                                            "Size" : 20892,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 196,
                                                "G" : 144,
                                                "B" : 7
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/War_of_the_Worlds_(2005_film)",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "A2947ea4834a58bfb55e8577656e083d2",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "War of the Worlds",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "war of the worlds 2005 movie",
                                        "QueryText" : "war of the worlds 2005 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "war of the worlds 2005"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "6fcc63d9-0c21-cd9f-d0f6-70d95819746b"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "War of the Worlds is a 2005 American science fiction disaster film directed by Steven Spielberg and written by Josh Friedman and David Koepp, loosely based on the novel of the same title by H. G. Wells. It stars Tom Cruise, Dakota Fanning, Justin Chatwin, Miranda Otto and Tim Robbins, with narration by Morgan Freeman. In the film, an American dock worker is forced to look after his children, from whom he lives separately, and struggles to protect them and reunite them with their mother when extraterrestrials invade the Earth and devastate cities with towering war machines.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2005",
                                "Description" : "Tom Cruise played Ray Ferrier in War of the Worlds.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2005
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Ray Ferrier",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "ray ferrier",
                                                "QueryText" : "ray ferrier",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "ray ferrier"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "8d4d6b79-7c61-091e-d137-ae034c263efd"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in War of the Worlds."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.BasicEntity[2.10]",
                                    "SatoriId" : "44261d2a-ab18-83e5-1a15-9aec17b5f5a0",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNDE5NjQzMDkzOF5BMl5BanBnXkFtZTcwODI3ODI3MQ@@._V1_UY268_CR4,0,182,268_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/title/tt0942385/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 182,
                                            "Height" : 268,
                                            "Format" : 1,
                                            "Size" : 18730,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 181,
                                                "G" : 139,
                                                "B" : 22
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/title/tt0942385/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Aae0a2eaeafc58454507b4aebfc59d23a",
                                        "ThumbnailWidth" : 182,
                                        "ThumbnailHeight" : 268,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    3
                                                ]
                                            },
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Tropic Thunder",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "tropic thunder 2008 movie",
                                        "QueryText" : "tropic thunder 2008 movie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "tropic thunder 2008"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "44261d2a-ab18-83e5-1a15-9aec17b5f5a0"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_645a7530"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Tropic Thunder is a 2008 American satirical action comedy film co-written, produced, directed by, and co-starring Ben Stiller. The film also co-stars Robert Downey, Jr. and Jack Black. The main plot revolves around a group of prima donna actors who are making a fictional Vietnam War film. When their frustrated director decides to drop them in the middle of a jungle, they are forced to rely on their acting skills in order to survive the real action and danger. Written by Stiller, Justin Theroux and Etan Cohen, the film was produced by Red Hour Films and distributed by Paramount Pictures through DreamWorks Pictures.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "2008",
                                "Description" : "Tom Cruise played Les Grossman - Grossman's Office in Tropic Thunder.",
                                "AssociationTimeRange" : {
                                    "IsCurrent" : false,
                                    "Start" : {
                                        "Kif.Schema" : "Entities.Scalar.DateTime[2.7]",
                                        "Year" : 2008
                                    }
                                },
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Cruise played "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Les Grossman - Grossman's Office",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "les grossman - grossman's office",
                                                "QueryText" : "les grossman - grossman's office",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "les grossman - grossman's office"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "561e4909-40f7-94ae-15df-416bfe4e03ce"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " in Tropic Thunder."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "6d7d66a7-2cb8-0ae9-637c-f81fd749dc9a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjA1MjE2MTQ2MV5BMl5BanBnXkFtZTcwMjE5MDY0Nw@@._V1._SX640_SY962_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000093/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 640,
                                            "Height" : 962,
                                            "Format" : 1,
                                            "Size" : 86605,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 96,
                                                "G" : 102,
                                                "B" : 107
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000093/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A088020972c3b97680bb4e324c760ba66",
                                        "ThumbnailWidth" : 199,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Brad Pitt",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "brad pitt",
                                        "QueryText" : "brad pitt",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "brad pitt"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "6d7d66a7-2cb8-0ae9-637c-f81fd749dc9a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "William Bradley \"Brad\" Pitt is an American actor and producer. He has received a Golden Globe Award, a Screen Actors Guild Award, and three Academy Award nominations in acting categories, and received three further Academy Award nominations, winning one, as producer under his own company Plan B Entertainment.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Brad Pitt and Tom Cruise both appear in Interview with the Vampire: The Vampire Chronicles.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Brad Pitt and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Interview with the Vampire: The Vampire Chronicles",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "interview with the vampire movie",
                                                "QueryText" : "interview with the vampire movie",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "interview with the vampire movie"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "824efa6b-eaab-87e8-ecd1-04d60bff0486"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "64ed04ee-734f-6a89-a646-8340f50d3f3a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://www.picsimages.net/photo/emily-blunt/emily-blunt_1336348136.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 672,
                                            "Height" : 1024,
                                            "Format" : 1,
                                            "Size" : 112334,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 84,
                                                "G" : 86,
                                                "B" : 79
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "picsimages",
                                            "Url" : {
                                                "Url" : "http://www.picsimages.net/452_emily-blunt/",
                                                "DisplayName" : "picsimages"
                                            }
                                        },
                                        "ThumbnailId" : "A6206b39cf4626f3c262628840357c443",
                                        "ThumbnailWidth" : 196,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Emily Blunt",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "emily blunt",
                                        "QueryText" : "emily blunt",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "emily blunt"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "64ed04ee-734f-6a89-a646-8340f50d3f3a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Emily Blunt and Tom Cruise both appear in Edge of Tomorrow.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Emily Blunt and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Edge of Tomorrow",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "edge of tomorrow film",
                                                "QueryText" : "edge of tomorrow film",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "edge of tomorrow film"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "69f45dcd-e8d5-7b44-c8e5-083eb2d139c8"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "5f2c4ac2-d77a-474e-4580-c76549d4fce5",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMjI0MTg3MzI0M15BMl5BanBnXkFtZTcwMzQyODU2Mw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000138/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13028,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 81,
                                                "G" : 89,
                                                "B" : 104
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000138/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A8a236ec4ef6c8be18e7cdb72a51db309",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Leonardo DiCaprio",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "leonardo dicaprio",
                                        "QueryText" : "leonardo dicaprio",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "leonardo dicaprio"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "5f2c4ac2-d77a-474e-4580-c76549d4fce5"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Leonardo Wilhelm DiCaprio is an American actor and film producer. In the early 1990s, DiCaprio began his career by appearing in television commercials, after which he had recurring roles in TV series such as the soap opera Santa Barbara and the sitcom, Growing Pains. In 1993, DiCaprio began his film career by starring as Josh in Critters 3, before starring in the film adaptation of the memoir This Boy's Life alongside Robert De Niro. DiCaprio was praised for his supporting role in the drama What's Eating Gilbert Grape, and gained public recognition with leading roles in the drama The Basketball Diaries, and the romantic drama Romeo + Juliet, before achieving international fame with James Cameron's epic romance Titanic, which became the highest-grossing film to that point.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Leonardo DiCaprio and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Leonardo DiCaprio and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "69fa0ad5-f1e0-ba58-1c3e-afab20b7e577",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://1.bp.blogspot.com/-9R4Z6qXfHmM/T6V5adspEOI/AAAAAAAAEVw/Yav4NDqPaVc/s1600/Johnny+Depp.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 1024,
                                            "Height" : 768,
                                            "Format" : 1,
                                            "Size" : 113931,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 48,
                                                "G" : 77,
                                                "B" : 99
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "theaceblackblog",
                                            "Url" : {
                                                "Url" : "http://www.theaceblackblog.com/2012/05/movies-of-johnny-depp.html",
                                                "DisplayName" : "theaceblackblog"
                                            }
                                        },
                                        "ThumbnailId" : "Afcfb547ba82e5a5c409aece1bff747ca",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 225,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Johnny Depp",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "johnny depp",
                                        "QueryText" : "johnny depp",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "johnny depp"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "69fa0ad5-f1e0-ba58-1c3e-afab20b7e577"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "John Christopher \"Johnny\" Depp II is an American actor, producer, and musician. He has won the Golden Globe Award and Screen Actors Guild Award for Best Actor. He rose to prominence on the 1980s television series 21 Jump Street, becoming a teen idol.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Johnny Depp and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Johnny Depp and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "47548b76-d91c-5710-e5b2-2c9d709a3be9",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTQ2MjMwNDA3Nl5BMl5BanBnXkFtZTcwMTA2NDY3NQ@@._V1_UY317_CR2,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000158/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 12584,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 167,
                                                "G" : 123,
                                                "B" : 36
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000158/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Acedb2a10e1710c5553ebbfb7b6c43db3",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Tom Hanks",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "tom hanks",
                                        "QueryText" : "tom hanks",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "tom hanks"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "47548b76-d91c-5710-e5b2-2c9d709a3be9"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Thomas Jeffrey \"Tom\" Hanks is an American actor and filmmaker. He is known for his roles in Splash, Big, Turner & Hooch, Philadelphia, Forrest Gump, Apollo 13, Saving Private Ryan, You've Got Mail, The Green Mile, Cast Away, The Da Vinci Code, Captain Phillips, and Saving Mr. Banks, as well as for his voice work in the animated films The Polar Express and the Toy Story series.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Tom Hanks and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tom Hanks and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "995c9cff-662a-34d5-3950-5ba648ed216a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://cdn.infos.fr/wp-content/uploads/2013/02/bruce-willis.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 600,
                                            "Height" : 600,
                                            "Format" : 1,
                                            "Size" : 318022,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 153,
                                                "G" : 62,
                                                "B" : 50
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "infos",
                                            "Url" : {
                                                "Url" : "http://www.infos.fr/le-ministere-de-la-culture-honore-bruce-willis-12562.html",
                                                "DisplayName" : "infos"
                                            }
                                        },
                                        "ThumbnailId" : "A844d5a2ea18e749c3a0335ed09a6e910",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Bruce Willis",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "bruce willis",
                                        "QueryText" : "bruce willis",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "bruce willis"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "995c9cff-662a-34d5-3950-5ba648ed216a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Walter Bruce Willis, known professionally as Bruce Willis, is an American actor, producer, and singer. His career began on the Off-Broadway stage and then in television in the 1980s, most notably as David Addison in Moonlighting. He is perhaps best known for his role of John McClane in the Die Hard series. He has appeared in over 60 films, including Color of Night, Pulp Fiction, 12 Monkeys, The Fifth Element, Armageddon, The Sixth Sense, Unbreakable, Sin City, Red, The Expendables 2, and Looper.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Bruce Willis and Tom Cruise are both Golden Raspberry Awards winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Bruce Willis and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Raspberry Awards",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "razzies",
                                                "QueryText" : "razzies",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "razzies"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "166c302a-64cf-0a36-bd21-de9142c9bb8d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "f1742a8c-3986-a53c-f9ed-bc15f3d2eb42",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BODg3MzYwMjE4N15BMl5BanBnXkFtZTcwMjU5NzAzNw@@._V1_UY317_CR22,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0001401/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 14426,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 102,
                                                "G" : 102,
                                                "B" : 102
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0001401/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A3a8f90e632fc667960b68077fb118f2e",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Angelina Jolie",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "angelina jolie",
                                        "QueryText" : "angelina jolie",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "angelina jolie"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "f1742a8c-3986-a53c-f9ed-bc15f3d2eb42"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Angelina Jolie and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Angelina Jolie and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "d21de4be-a93d-3c20-fcda-6903b067e20f",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTkxNTI5NzM4MV5BMl5BanBnXkFtZTcwMTI3ODY3Mg@@._V1_UY317_CR0,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000139/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 16657,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 185,
                                                "G" : 142,
                                                "B" : 18
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000139/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A090e5b8fa1a68c2ebd39c4dc8a95de65",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Cameron Diaz",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "cameron diaz",
                                        "QueryText" : "cameron diaz",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "cameron diaz"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "d21de4be-a93d-3c20-fcda-6903b067e20f"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Cameron Michelle Díaz is an American actress, producer, and former fashion model. She rose to stardom with roles in The Mask, My Best Friend's Wedding and There's Something About Mary, and is also known for voicing the character of Princess Fiona in the Shrek series. Other high-profile credits include Charlie's Angels and its sequel Charlie's Angels: Full Throttle, The Sweetest Thing, In Her Shoes, The Holiday, What Happens In Vegas, My Sister's Keeper, Knight and Day, The Green Hornet, Bad Teacher, What to Expect When You're Expecting, The Counselor, The Other Woman, Sex Tape, and Annie.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Cameron Diaz and Tom Cruise both appear in Knight and Day.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Cameron Diaz and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Knight and Day",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "knight and day 2010",
                                                "QueryText" : "knight and day 2010",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "knight and day 2010"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "990c900c-6c74-863a-170e-1e3e3b18f09e"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "ee4ac44e-f1b1-103d-1a41-6b281462b58c",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://www.biography.com/imported/images/Biography/Images/Profiles/S/Will-Smith-9542165-1-402.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 402,
                                            "Height" : 402,
                                            "Format" : 1,
                                            "Size" : 49332,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 59,
                                                "G" : 104,
                                                "B" : 144
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "biography",
                                            "Url" : {
                                                "Url" : "http://www.biography.com/people/will-smith-9542165",
                                                "DisplayName" : "biography"
                                            }
                                        },
                                        "ThumbnailId" : "A5ca096aeeff225ef6c4cb80f2639dc85",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Will Smith",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "will smith",
                                        "QueryText" : "will smith",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "will smith"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "ee4ac44e-f1b1-103d-1a41-6b281462b58c"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Willard Carroll \"Will\" Smith, Jr. is an American actor, comedian, producer, rapper, and songwriter. He has enjoyed success in television, film, and music. In April 2007, Newsweek called him \"the most powerful actor in Hollywood\". Smith has been nominated for five Golden Globe Awards, two Academy Awards, and has won four Grammy Awards.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Will Smith and Tom Cruise both appear in Top Gear.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Will Smith and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Top Gear",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "top gear",
                                                "QueryText" : "top gear",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "top gear"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "684e1cfe-24e4-56d2-5280-79a9f735d4c0"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b02d3a12-c8e7-2b68-24c4-eb061803d63a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/a/a4/Sylvester_Stallone_2012.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 509,
                                            "Height" : 720,
                                            "Format" : 1,
                                            "Size" : 81978,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 82,
                                                "G" : 112,
                                                "B" : 121
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Sylvester_Stallone",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "Ac7252f38328ca4a20224e7a5d55c5f88",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Sylvester Stallone",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "sylvester stallone",
                                        "QueryText" : "sylvester stallone",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "sylvester stallone"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b02d3a12-c8e7-2b68-24c4-eb061803d63a"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Sylvester Gardenzio Stallone is an American actor, screenwriter, producer, and director. He is well known for his Hollywood action roles, particularly boxer Rocky Balboa, the title character of the Rocky series' seven films from 1976 to 2015; soldier John Rambo from the four Rambo films, which ran from 1982 to 2008; and Barney Ross in the three The Expendables films from 2010 to 2014. He wrote or co-wrote most of the 14 films in all three franchises, and directed many of the films.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Sylvester Stallone and Tom Cruise are both Golden Raspberry Awards winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Sylvester Stallone and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Raspberry Awards",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "razzies",
                                                "QueryText" : "razzies",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "razzies"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "166c302a-64cf-0a36-bd21-de9142c9bb8d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "c0eec860-975d-200e-8592-3749e58012d2",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTM0NzYzNDgxMl5BMl5BanBnXkFtZTcwMDg2MTMyMw@@._V1_SY317_CR11,0,214,317_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000354",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 11570,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 100,
                                                "G" : 39,
                                                "B" : 37
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000354",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A1cfb38f17f38c7ff0d441fa1e57e0db0",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Matt Damon",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "matt damon",
                                        "QueryText" : "matt damon",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "matt damon"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "c0eec860-975d-200e-8592-3749e58012d2"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Matthew Paige \"Matt\" Damon is an American actor and filmmaker. Born and raised in Cambridge, Massachusetts, Damon began his acting career by appearing in high school theater productions and he made his professional acting debut in the film Mystic Pizza. He came to prominence in 1997 when he wrote and starred in Good Will Hunting alongside Ben Affleck, which won them the Academy Award for Best Original Screenplay and the Golden Globe Award for Best Screenplay and earned Damon a nomination for the Academy Award for Best Actor. He received further praise for his roles as the eponymous character in Saving Private Ryan, the antihero in The Talented Mr. Ripley, a fallen angel in Dogma, and received critical acclaim for performances in dramas such as Syriana and The Good Shepherd, as well as his role as a villain in the neo-noir crime drama The Departed.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Matt Damon and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Matt Damon and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "e82ae937-4cd9-37d1-cae7-49e313ff8414",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://upload.wikimedia.org/wikipedia/commons/d/d4/Arnold_Schwarzenegger_February_2015.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 899,
                                            "Height" : 1245,
                                            "Format" : 1,
                                            "Size" : 824653,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 67,
                                                "B" : 91
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "wikipedia",
                                            "Url" : {
                                                "Url" : "http://en.wikipedia.org/wiki/Arnold_Schwarzenegger",
                                                "DisplayName" : "wikipedia"
                                            }
                                        },
                                        "ThumbnailId" : "Aef9f8517e5690e4310f7bdb871a5bfcc",
                                        "ThumbnailWidth" : 216,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Arnold Schwarzenegger",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "arnold schwarzenegger",
                                        "QueryText" : "arnold schwarzenegger",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "arnold schwarzenegger"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "e82ae937-4cd9-37d1-cae7-49e313ff8414"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Arnold Alois Schwarzenegger is an Austrian-American actor, filmmaker, businessman, investor, author, philanthropist, activist, former professional bodybuilder and former politician. He served two terms as the 38th Governor of California from 2003 until 2011.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Arnold Schwarzenegger and Tom Cruise are both Golden Raspberry Awards winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Arnold Schwarzenegger and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Raspberry Awards",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "razzies",
                                                "QueryText" : "razzies",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "razzies"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "166c302a-64cf-0a36-bd21-de9142c9bb8d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "13515c0a-a4eb-a57d-b17e-62511d7f4192",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTAwNjk2NTUyMzleQTJeQWpwZ15BbWU3MDQ2NzQzMTc@._V1_UY317_CR2,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000375/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13067,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 136,
                                                "G" : 67,
                                                "B" : 95
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000375/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Abf6638a2a45c4d65be41df8fbe18c69c",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Robert Downey Jr.",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "robert downey jr.",
                                        "QueryText" : "robert downey jr.",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "robert downey jr."
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "13515c0a-a4eb-a57d-b17e-62511d7f4192"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Robert John Downey Jr. is an American actor. His career has included critical and popular success in his youth, followed by a period of substance abuse and legal troubles, and a resurgence of commercial success in middle age.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Robert Downey Jr. and Tom Cruise both appear in Tropic Thunder.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Robert Downey Jr. and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Tropic Thunder",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "tropic thunder 2008",
                                                "QueryText" : "tropic thunder 2008",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "tropic thunder 2008"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "44261d2a-ab18-83e5-1a15-9aec17b5f5a0"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "61510d32-6e34-7bc5-5939-61aceb721b0e",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://www.fullnetworth.com/wp-content/uploads/2014/11/George-Clooney-Net-Worth.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 579,
                                            "Height" : 413,
                                            "Format" : 1,
                                            "Size" : 118483,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 27,
                                                "G" : 26,
                                                "B" : 29
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "fullnetworth",
                                            "Url" : {
                                                "Url" : "http://www.fullnetworth.com/george-clooney-net-worth/",
                                                "DisplayName" : "fullnetworth"
                                            }
                                        },
                                        "ThumbnailId" : "A17e80d2a939a45ecb0795b5f6d9e7ee0",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 213,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "George Clooney",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "george clooney",
                                        "QueryText" : "george clooney",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "george clooney"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "61510d32-6e34-7bc5-5939-61aceb721b0e"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "George Timothy Clooney is an American actor, filmmaker and activist. He has received three Golden Globe Awards for his work as an actor and two Academy Awards, one for acting and the other for producing.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "George Clooney and Tom Cruise are both Golden Globe Award winners.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "George Clooney and Tom Cruise are both "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Golden Globe Award",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "golden globe award",
                                                "QueryText" : "golden globe award",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "golden globe award"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "5197088b-13cf-3c52-cbb1-9bd20050ce4d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : " winners."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "93a10d3f-2008-7162-b066-a19ab20172fc",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://static1.purepeople.com/articles/0/68/35/0/@/513406-mark-wahlberg-637x0-3.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 519,
                                            "Height" : 800,
                                            "Format" : 1,
                                            "Size" : 54780,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 113,
                                                "G" : 62,
                                                "B" : 104
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "purepeople",
                                            "Url" : {
                                                "Url" : "http://www.purepeople.com/media/mark-wahlberg_m513406",
                                                "DisplayName" : "purepeople"
                                            }
                                        },
                                        "ThumbnailId" : "A6f0ca8e27a80d9bd7f79e0539dba7e4b",
                                        "ThumbnailWidth" : 194,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mark Wahlberg",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mark wahlberg",
                                        "QueryText" : "mark wahlberg",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mark wahlberg"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "93a10d3f-2008-7162-b066-a19ab20172fc"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Mark Robert Michael Wahlberg is an American actor, producer, businessman, and former model and rapper. He was known as Marky Mark in his earlier years, as frontman with the band Marky Mark and the Funky Bunch, releasing the albums Music for the People and You Gotta Believe. Wahlberg later transitioned to acting, appearing in films such as the drama Boogie Nights and the satirical war comedy-drama Three Kings during the 1990s. In the 2000s, he starred in the biographical disaster drama The Perfect Storm, the science-fiction film Planet of the Apes, and the Martin Scorsese-directed neo-noir crime drama The Departed. In the 2010s, he starred in the action-comedy The Other Guys alongside Will Ferrell, the biographical sports drama The Fighter, the comedy Ted, the war film Lone Survivor, and the science-fiction action film Transformers: Age of Extinction.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Mark Wahlberg and Tom Cruise both appear in Top Gear.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Mark Wahlberg and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Top Gear",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "top gear",
                                                "QueryText" : "top gear",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "top gear"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "684e1cfe-24e4-56d2-5280-79a9f735d4c0"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "1c218438-43c4-d2cf-72e1-1feda0c7c526",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://www.starscolor.com/images/kelly-mcgillis-02.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 416,
                                            "Height" : 440,
                                            "Format" : 1,
                                            "Size" : 29396,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 15,
                                                "G" : 44,
                                                "B" : 147
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "starscolor",
                                            "Url" : {
                                                "Url" : "http://www.starscolor.com/525-kelly-mcgillis/",
                                                "DisplayName" : "starscolor"
                                            }
                                        },
                                        "ThumbnailId" : "A8bf1f305c69c464c146ed095306d8f88",
                                        "ThumbnailWidth" : 283,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Kelly McGillis",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "kelly mcgillis",
                                        "QueryText" : "kelly mcgillis",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "kelly mcgillis"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "1c218438-43c4-d2cf-72e1-1feda0c7c526"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Kelly Ann McGillis is an American actress. She has found fame for her acting roles in several films since the 1980s including: her role as Rachel Lapp in Witness with Harrison Ford, for which she received Golden Globe and BAFTA nominations, the role of Charlie in the huge Blockbuster hit Top Gun with Tom Cruise, and the role of attorney Kathryn Murphy in The Accused, with Jodie Foster.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Kelly McGillis and Tom Cruise both appear in Top Gun.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Kelly McGillis and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Top Gun",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "top gun 1986",
                                                "QueryText" : "top gun 1986",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "top gun 1986"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "693f9bdc-21a9-98bb-5f70-9e0556ad047d"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            },
                            {
                                "Kif.Schema" : "Entities.GeneralRelationship[2.10]",
                                "RelatedEntity" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "01a19cfe-008e-543d-4bfa-d84505d4ff76",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BNDExMzIzNjk3Nl5BMl5BanBnXkFtZTcwOTE4NDU5OA@@._V1_SX214_CR0,0,214,317_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0413168",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13152,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 36,
                                                "G" : 28,
                                                "B" : 28
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0413168",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A9ea6093b9e46ea379073de119f060bc6",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "DisableCropping" : false,
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 12,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Hugh Jackman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "hugh jackman",
                                        "QueryText" : "hugh jackman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "hugh jackman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "01a19cfe-008e-543d-4bfa-d84505d4ff76"
                                                ]
                                            },
                                            {
                                                "Name" : "catguid",
                                                "Values" : [
                                                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c_cfb02057"
                                                ]
                                            },
                                            {
                                                "Name" : "segment",
                                                "Values" : [
                                                    "generic.carousel"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Hugh Michael Jackman is an Australian actor, producer and musician. Jackman has won international recognition for his roles in major films, notably as superhero, period, and romance characters. He is best known for his long-running role as Wolverine in the X-Men film series, as well as for his lead roles in the romantic-comedy fantasy Kate & Leopold, the action-horror film Van Helsing, the magic-themed drama The Prestige, the epic historical romantic drama Australia, the sci-fi sports drama Real Steel, the film version of Les Misérables, and the thriller Prisoners. His work in Les Misérables earned him his first Academy Award nomination for Best Actor and his first Golden Globe Award for Best Actor – Motion Picture Musical or Comedy in 2013.",
                                        "SeeMoreUrl" : {
                                            "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                            "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                                        }
                                    }
                                },
                                "Relationship" : "",
                                "Description" : "Hugh Jackman and Tom Cruise both appear in Top Gear.",
                                "DescriptionScalar" : {
                                    "Text" : "",
                                    "Fragments" : [
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Hugh Jackman and Tom Cruise both appear in "
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "Top Gear",
                                            "SeeMoreQuery" : {
                                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                                "QueryUrlText" : "top gear",
                                                "QueryText" : "top gear",
                                                "QueryParams" : [
                                                    {
                                                        "Name" : "ufn",
                                                        "Values" : [
                                                            "top gear"
                                                        ]
                                                    },
                                                    {
                                                        "Name" : "sid",
                                                        "Values" : [
                                                            "684e1cfe-24e4-56d2-5280-79a9f735d4c0"
                                                        ]
                                                    }
                                                ]
                                            }
                                        },
                                        {
                                            "Kif.Schema" : "Entities.Scalar.DataFragment[2.7]",
                                            "Text" : "."
                                        }
                                    ]
                                }
                            }
                        ],
                        "Gender" : 0,
                        "DateOfBirth" : {
                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                            "Year" : 1962,
                            "Month" : 7,
                            "Day" : 3
                        },
                        "Height" : {
                            "Kif.Schema" : "Entities.Scalar.Length[2.6]",
                            "Value" : 1.70000004768372,
                            "NumericType" : 2,
                            "System" : 1,
                            "Unit" : 6
                        },
                        "Professions" : [
                            {
                                "Name" : "Actor"
                            },
                            {
                                "Name" : "Screenwriter"
                            },
                            {
                                "Name" : "Film Producer"
                            },
                            {
                                "Name" : "Television Director"
                            }
                        ],
                        "Spouses" : [
                            {
                                "Person" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "1ee20f46-a32e-d538-eaf6-95e1d45b41a6",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://img2.timeinc.net/instyle/images/2012/TRANSFORMATIONS/2012-katie-holmes-400.jpg",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 400,
                                            "Height" : 400,
                                            "Format" : 1,
                                            "Size" : 45633,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 146,
                                                "G" : 63,
                                                "B" : 57
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "pinterest",
                                            "Url" : {
                                                "Url" : "http://pinterest.com/pin/186336503302739206/",
                                                "DisplayName" : "pinterest"
                                            }
                                        },
                                        "ThumbnailId" : "A02d312304864ebf4fcdea77ceed584c7",
                                        "ThumbnailWidth" : 300,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Katie Holmes",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "katie holmes",
                                        "QueryText" : "katie holmes",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "katie holmes"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "1ee20f46-a32e-d538-eaf6-95e1d45b41a6"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1978,
                                        "Month" : 12,
                                        "Day" : 18
                                    }
                                },
                                "Periods" : [
                                    {
                                        "IsCurrent" : false,
                                        "Start" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 2006,
                                            "Month" : 11,
                                            "Day" : 18
                                        },
                                        "End" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 2012,
                                            "Month" : 8,
                                            "Day" : 20
                                        }
                                    }
                                ]
                            },
                            {
                                "Person" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "162b6ca3-a565-7fa4-e407-d443009e098a",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM5NDg4MF5BMl5BanBnXkFtZTcwNDg1OTQ4Nw@@._V1_UY317_CR10,0,214,317_AL_.jpg",
                                        "PageUrl" : "http://www.imdb.com/name/nm0000173/",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 214,
                                            "Height" : 317,
                                            "Format" : 1,
                                            "Size" : 13427,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 63,
                                                "G" : 110,
                                                "B" : 140
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/name/nm0000173/",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "A6a462a2013209bbb165f5e9c238b1439",
                                        "ThumbnailWidth" : 202,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Nicole Kidman",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "nicole kidman",
                                        "QueryText" : "nicole kidman",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "nicole kidman"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "162b6ca3-a565-7fa4-e407-d443009e098a"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : "Nicole Mary Kidman, AC is an Australian actress and film producer. Kidman's breakthrough roles were in the 1989 feature film thriller Dead Calm and television thriller miniseries Bangkok Hilton. Appearing in several films in the early 1990s, she came to worldwide recognition for her performances in the stock-car racing film Days of Thunder, the romance-drama Far and Away, and the superhero film Batman Forever. Other successful films followed in the late 1990s. Her performance in the musical Moulin Rouge! earned her a second Golden Globe Award for Best Actress – Motion Picture Musical or Comedy and her first nomination for the Academy Award for Best Actress. Kidman's performance as Virginia Woolf in the drama film The Hours received critical acclaim and earned her the Academy Award for Best Actress, the BAFTA Award for Best Actress in a Leading Role, the Golden Globe Award for Best Actress – Motion Picture Drama and the Silver Bear for Best Actress at the Berlin International Film Festival."
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1967,
                                        "Month" : 6,
                                        "Day" : 20
                                    }
                                },
                                "Periods" : [
                                    {
                                        "IsCurrent" : false,
                                        "Start" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 1990,
                                            "Month" : 12,
                                            "Day" : 24
                                        },
                                        "End" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 2001,
                                            "Month" : 8,
                                            "Day" : 8
                                        }
                                    }
                                ]
                            },
                            {
                                "Person" : {
                                    "Kif.Schema" : "Entities.Person[2.12]",
                                    "SatoriId" : "b1966cee-2daa-e277-94d9-171eec9f7045",
                                    "Image" : {
                                        "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                                        "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTYzOTkxMTc2M15BMl5BanBnXkFtZTcwODQwNTQ0NA@@._V1._SX640_SY924_.jpg",
                                        "PageUrl" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                        "SourceImageProperties" : {
                                            "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                            "Width" : 640,
                                            "Height" : 924,
                                            "Format" : 1,
                                            "Size" : 82598,
                                            "AccentColor" : {
                                                "A" : 255,
                                                "R" : 160,
                                                "G" : 54,
                                                "B" : 43
                                            }
                                        },
                                        "Provider" : {
                                            "Kif.Schema" : "Entities.Common.DataProvider[2.7]",
                                            "Name" : "imdb",
                                            "Url" : {
                                                "Url" : "http://www.imdb.com/media/rm1708243456/nm0000211",
                                                "DisplayName" : "imdb"
                                            }
                                        },
                                        "ThumbnailId" : "Ac17e6758cd37d02310b550f0a5b99b32",
                                        "ThumbnailWidth" : 207,
                                        "ThumbnailHeight" : 300,
                                        "PartnerId" : "16.1",
                                        "ThumbnailRenderingInfo" : {
                                            "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                            "ImageTypes" : {
                                                "Kif.Type" : "typedList",
                                                "Kif.ElementType" : "enum",
                                                "Kif.Value" : [
                                                    1
                                                ]
                                            },
                                            "SuggestedCroppingParameter" : 7,
                                            "SuggestedThumbnailPartnerId" : "16.2",
                                            "IsLogo" : false
                                        }
                                    },
                                    "Name" : "Mimi Rogers",
                                    "SeeMoreQuery" : {
                                        "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                        "QueryUrlText" : "mimi rogers",
                                        "QueryText" : "mimi rogers",
                                        "QueryParams" : [
                                            {
                                                "Name" : "ufn",
                                                "Values" : [
                                                    "mimi rogers"
                                                ]
                                            },
                                            {
                                                "Name" : "sid",
                                                "Values" : [
                                                    "b1966cee-2daa-e277-94d9-171eec9f7045"
                                                ]
                                            }
                                        ],
                                        "SnapshotExists" : true
                                    },
                                    "Description" : {
                                        "Kif.Schema" : "Entities.Common.Description[2.8]",
                                        "Text" : ""
                                    },
                                    "DateOfBirth" : {
                                        "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                        "Year" : 1956,
                                        "Month" : 1,
                                        "Day" : 27
                                    }
                                },
                                "Periods" : [
                                    {
                                        "IsCurrent" : false,
                                        "Start" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 1987,
                                            "Month" : 5,
                                            "Day" : 9
                                        },
                                        "End" : {
                                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                            "Year" : 1990,
                                            "Month" : 2,
                                            "Day" : 4
                                        }
                                    }
                                ]
                            }
                        ],
                        "PlaceOfBirth" : {
                            "Kif.Schema" : "Entities.Place[2.15]",
                            "SatoriId" : "5232ed96-85b1-2edb-12c6-63e6c597a1de",
                            "Name" : "United States",
                            "SeeMoreQuery" : {
                                "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                "QueryUrlText" : "united states",
                                "QueryText" : "united states",
                                "QueryParams" : [
                                    {
                                        "Name" : "ufn",
                                        "Values" : [
                                            "united states"
                                        ]
                                    },
                                    {
                                        "Name" : "sid",
                                        "Values" : [
                                            "5232ed96-85b1-2edb-12c6-63e6c597a1de"
                                        ]
                                    }
                                ],
                                "SnapshotExists" : true
                            },
                            "Description" : {
                                "Kif.Schema" : "Entities.Common.Description[2.8]",
                                "Text" : ""
                            }
                        },
                        "LanguagesSpoken" : [
                            {
                                "Kif.Schema" : "Entities.Language[2.10]",
                                "SatoriId" : "757135ff-b971-4bd0-ab77-8b962c22692c",
                                "Name" : "English",
                                "SeeMoreQuery" : {
                                    "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                                    "QueryUrlText" : "english language",
                                    "QueryText" : "english language",
                                    "QueryParams" : [
                                        {
                                            "Name" : "ufn",
                                            "Values" : [
                                                "english language"
                                            ]
                                        },
                                        {
                                            "Name" : "sid",
                                            "Values" : [
                                                "757135ff-b971-4bd0-ab77-8b962c22692c"
                                            ]
                                        }
                                    ]
                                }
                            }
                        ]
                    },
                    "InstrumentationInfo" : [
                        {
                            "Kif.Schema" : "Entities.Instrumentation.EntityInfo[2.6]",
                            "DataSources" : [
                                "ADELE: Remove DataGroup cdb:datagroupid.readout for segment All",
                                "ADELE: Remove DataGroup rel/films|True"
                            ],
                            "ConfidenceScore" : 0
                        }
                    ],
                    "SegmentTypes" : [
                        "Actor"
                    ],
                    "SatoriTypes" : [
                        "mso:people.person",
                        "mso:event.agent",
                        "mso:film.actor",
                        "mso:biology.organism",
                        "mso:music.artist",
                        "mso:film.writer",
                        "mso:tv.actor",
                        "mso:tv.director",
                        "mso:award.nominee",
                        "mso:ratings.rated_entity",
                        "mso:film.film_song_performer",
                        "mso:award.winner",
                        "mso:book.subject",
                        "mso:celebrities.celebrity",
                        "mso:fictional_universe.person_in_fiction",
                        "mso:film.producer",
                        "mso:film.story_contributor",
                        "mso:food.diet_follower",
                        "mso:media_common.subject",
                        "mso:medicine.notable_person_with_medical_condition",
                        "mso:organization.founder",
                        "mso:projects.project_participant",
                        "mso:tv.personality",
                        "mso:tv.producer"
                    ],
                    "AppContents" : {
                        "ResultLists" : [
                        ]
                    }
                }
            ],
            "EntityScenario" : 0,
            "QContext" : {
                "Kif.Schema" : "Entities.QueryContext[2.33]",
                "DebugInfo" : [
                    "Webcaptions",
                    "SFObjectStore - SFEP.captionscombined2",
                    "EditorialIdBlackListObjectStore - Satori.GDIEditorialTriggering_BlackList",
                    "TopEntityStore - Satori.EntityPane_ID_Content.v3top; From plugin: 0",
                    "BigEntityStore - Satori.EntityPane_ID_Content.v3big; From plugin: 1",
                    "GenericEntityStore - Satori.EntityPane_ID_Content.v3big.generic; From plugin: 2",
                    "DisambigStore - Satori.EntityPane_ID_Content.v3elvtop; From plugin: 3",
                    "BigDisambigStore - Satori.EntityPane_ID_Content.v3elvbig; From plugin: 4",
                    "GenericDisambigEntityStore - Satori.EntityPane_ID_Content.v3elvbig.generic; From plugin: 5",
                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c - TopEntityStore - Satori.EntityPane_ID_Content.v3top - No SF Update - response_idx=0",
                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f - TopEntityStore - Satori.EntityPane_ID_Content.v3top - No SF Update - response_idx=0",
                    "fdc9ba0e-f198-b3c3-3cad-cf81595a654c - DisambigStore - Satori.EntityPane_ID_Content.v3elvtop - No SF Update - response_idx=3",
                    "f6a9fd2b-4d99-51cb-592d-2da51f13a67f - DisambigStore - Satori.EntityPane_ID_Content.v3elvtop - No SF Update - response_idx=3"
                ]
            },
            "DisambigContainers" : [
                {
                    "Kif.Schema" : "Entities.Containment.EntityContainer[2.18]",
                    "DataGroupContainer" : {
                        "DataGroups" : [
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 3,
                                    "Context" : 1,
                                    "Key" : "cdb:datagroupid.entityitem",
                                    "FriendlyName" : "See results for",
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.entityitem",
                                        "IsPlural" : true,
                                        "FriendlyName" : "See results for"
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.image",
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/Image",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Image"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "k:ext/EntityTypeRanking.DomType.DomType",
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/DominantType",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DominantType"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:type.object.description",
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/Description",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:Description"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.synthetic.born",
                                            "IsPlural" : true,
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "Name" : "Born",
                                        "Key" : "cdb:property.synthetic.born",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.born",
                                                    "FriendlyName" : "Born"
                                                },
                                                "XPath" : "/EntityContent/DateOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:DateOfBirth"
                                            },
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "cdb:property.person.place_of_birth",
                                                    "FriendlyName" : "Birthplace"
                                                },
                                                "XPath" : "/EntityContent/PlaceOfBirth",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:PlaceOfBirth"
                                            }
                                        ]
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyGroup[2.13]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "mso:people.person.networth",
                                            "IsPlural" : true,
                                            "FriendlyName" : "",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "Name" : "Net worth",
                                        "Key" : "mso:people.person.networth",
                                        "IsPlural" : true,
                                        "Properties" : [
                                            {
                                                "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                                "MetaInformation" : {
                                                    "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                                },
                                                "IdentityInfo" : {
                                                    "CanonicalKey" : "mso:people.person.networth",
                                                    "IsPlural" : true,
                                                    "FriendlyName" : "Net worth"
                                                },
                                                "XPath" : "/EntityContent/RelatedScalars/*[1]",
                                                "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/0"
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "Kif.Schema" : "Entities.Grouping.DataGroup[2.17]",
                                "Info" : {
                                    "Kif.Schema" : "Entities.Grouping.DataGroupInfo[2.20]",
                                    "DisplayHint" : 46,
                                    "Context" : 5,
                                    "Key" : "cdb:datagroupid.extra_info",
                                    "FriendlyName" : "Extra Info",
                                    "IdentityInfo" : {
                                        "CanonicalKey" : "cdb:datagroupid.extra_info",
                                        "IsPlural" : true,
                                        "FriendlyName" : "Extra Info"
                                    },
                                    "Visibility" : {
                                        "IsVisible" : false
                                    }
                                },
                                "Properties" : [
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.official_website",
                                            "FriendlyName" : "Official Website"
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[3]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/2"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.wiki_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[4]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/3"
                                    },
                                    {
                                        "Kif.Schema" : "Entities.Grouping.PropertyPath[2.12]",
                                        "MetaInformation" : {
                                            "Kif.Schema" : "Entities.Common.MetaInformation[2.5]"
                                        },
                                        "IdentityInfo" : {
                                            "CanonicalKey" : "cdb:property.freebase_subject_key",
                                            "FriendlyName" : "subject key",
                                            "IsFriendlyNameLocalized" : true
                                        },
                                        "XPath" : "/EntityContent/RelatedScalars/*[2]",
                                        "EnhancedXPath" : "Entities.Containment.EntityContainer_2:EntityContent/Entities.Person_2:RelatedScalars/1"
                                    }
                                ]
                            }
                        ],
                        "Scenario" : "GenericEntityListItem"
                    },
                    "DebugData" : {
                        "DataVersion" : "5/15/2016 9:35 AM  2abb1979b9d6834cbf7443cc102334306278877e; Actor; aether://experiments/93f6beab-242e-494f-aebc-32b484275955; node id f5a67915; graph EntityDocuments.ss.2016-05-11_12.ss; target='ELV-XML' market='en-us' env='prod'"
                    },
                    "MetaInformation" : {
                        "Kif.Schema" : "Entities.Common.MetaInformation[2.5]",
                        "IsTopEntity" : true,
                        "Timestamp" : 635989017226982126,
                        "TraceId" : "",
                        "MetaInfoList" : [
                            {
                                "Key" : "threshold.mobile.bullseye.mop",
                                "Value" : "0.2199"
                            },
                            {
                                "Key" : "threshold.mobile.bullseye.top",
                                "Value" : "0.22"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.mop",
                                "Value" : "0.7999"
                            },
                            {
                                "Key" : "threshold.mobile.datagroup.top",
                                "Value" : "0.8"
                            },
                            {
                                "Key" : "threshold.desktop.datagroup.expand",
                                "Value" : "0.22"
                            },
                            {
                                "Key" : "threshold.build.bullseye",
                                "Value" : "0"
                            },
                            {
                                "Key" : "threshold.build.datagroup",
                                "Value" : "1E-06"
                            },
                            {
                                "Key" : "cdb:property.synthetic.born",
                                "Value" : "34|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.person.born",
                                "Value" : "32|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.synthetic.lived",
                                "Value" : "29|1.93910032436466"
                            },
                            {
                                "Key" : "cdb:property.person.height",
                                "Value" : "28|4.13490136883326"
                            },
                            {
                                "Key" : "mso:people.person.networth",
                                "Value" : "27|4.90911486967388"
                            },
                            {
                                "Key" : "cdb:property.person.spouses",
                                "Value" : "26|6.52065370074104"
                            },
                            {
                                "Key" : "cdb.person.domesticpartners",
                                "Value" : "25|6.52065370074104"
                            },
                            {
                                "Key" : "rel/person.romance|True",
                                "Value" : "24|6.52065370074104"
                            },
                            {
                                "Key" : "mso/people.person.children|True",
                                "Value" : "23|1.07074990903502"
                            },
                            {
                                "Key" : "cdb:datagroupid.experience",
                                "Value" : "22|0.00178857698896962"
                            },
                            {
                                "Key" : "rel/films_tvshows|True",
                                "Value" : "21|8.02122357316397"
                            },
                            {
                                "Key" : "rel/tvshows|True",
                                "Value" : "20|8.02122357316397"
                            },
                            {
                                "Key" : "rel/films|True",
                                "Value" : "19|8.02122357316397"
                            },
                            {
                                "Key" : "rel/person.romance.images|True",
                                "Value" : "18|6.52065370074104"
                            },
                            {
                                "Key" : "rel/films.upcoming|True",
                                "Value" : "17|0.346424286606398"
                            },
                            {
                                "Key" : "cdb:datagroupid.timeline",
                                "Value" : "16|0.261905756401386"
                            },
                            {
                                "Key" : "richnav_facebook",
                                "Value" : "15|0.205175748735764"
                            },
                            {
                                "Key" : "mso/award.nominee.award_nominations|True|mso/award.nomination.category|True",
                                "Value" : "14|0.189712109042382"
                            },
                            {
                                "Key" : "rel/artist.tracks|True",
                                "Value" : "13|0.156533097234671"
                            },
                            {
                                "Key" : "mso/people.person.parent|True",
                                "Value" : "12|0.155230053034844"
                            },
                            {
                                "Key" : "mso/people.person.sibling|True|mso/people.sibling_relationship.sibling|True",
                                "Value" : "11|0.147025045795492"
                            },
                            {
                                "Key" : "rel/person.awards|True",
                                "Value" : "10|0.117494816583309"
                            },
                            {
                                "Key" : "cdb:datagroupid.education",
                                "Value" : "9|0.106889617740407"
                            },
                            {
                                "Key" : "mso/people.person.education|True|mso/education.education.educational_institution|True",
                                "Value" : "8|0.106889617740407"
                            },
                            {
                                "Key" : "cdb:datagroupid.quotes",
                                "Value" : "7|0.0637114399510003"
                            },
                            {
                                "Key" : "mso/people.person.quotation|True",
                                "Value" : "6|0.0637114399510003"
                            },
                            {
                                "Key" : "mso/music.artist.album|True",
                                "Value" : "5|0.00322030190485263"
                            },
                            {
                                "Key" : "mso/organization.founder.organizations_founded|True",
                                "Value" : "4|0.00162584346921771"
                            },
                            {
                                "Key" : "mso:music.artist.active_start",
                                "Value" : "3|0.000749612250439422"
                            },
                            {
                                "Key" : "rel/sametypes|True",
                                "Value" : "2|0.746611732898386"
                            },
                            {
                                "Key" : "cdb:datagroupid.exploremore",
                                "Value" : "1|1E-05"
                            }
                        ]
                    },
                    "EntityContent" : {
                        "Kif.Schema" : "Entities.Person[2.12]",
                        "SatoriId" : "fdc9ba0e-f198-b3c3-3cad-cf81595a654c",
                        "Image" : {
                            "Kif.Schema" : "Entities.Imaging.BingImage[2.7]",
                            "SourceUrl" : "http://ia.media-imdb.com/images/M/MV5BMTk1MjM3NTU5M15BMl5BanBnXkFtZTcwMTMyMjAyMg@@._V1_UY317_CR14,0,214,317_AL_.jpg",
                            "Title" : "The thing about filmmaking is I give it everything, that's why I work so hard. I always tell young actors to take charge. It's not that hard. Sign your own checks, be responsible.",
                            "PageUrl" : "http://www.imdb.com/name/nm0000129/",
                            "SourceImageProperties" : {
                                "Kif.Schema" : "Entities.Imaging.ImageProperties[2.7]",
                                "Width" : 214,
                                "Height" : 317,
                                "Format" : 1,
                                "Size" : 13112,
                                "AccentColor" : {
                                    "A" : 255,
                                    "R" : 58,
                                    "G" : 94,
                                    "B" : 145
                                }
                            },
                            "ThumbnailId" : "Acf56e67b7cc2c2cbff9de810b1c93e0c",
                            "ThumbnailWidth" : 202,
                            "ThumbnailHeight" : 300,
                            "PartnerId" : "16.1",
                            "DisableCropping" : false,
                            "ThumbnailRenderingInfo" : {
                                "Kif.Schema" : "Entities.Imaging.ThumbnailRenderingInfo[2.4]",
                                "ImageTypes" : {
                                    "Kif.Type" : "typedList",
                                    "Kif.ElementType" : "enum",
                                    "Kif.Value" : [
                                        1
                                    ]
                                },
                                "SuggestedCroppingParameter" : 12,
                                "SuggestedThumbnailPartnerId" : "16.2",
                                "Segment" : "Actor",
                                "IsLogo" : false
                            }
                        },
                        "Name" : "Tom Cruise",
                        "SeeMoreQuery" : {
                            "Kif.Schema" : "Entities.Queries.InternalQuery[2.7]",
                            "QueryUrlText" : "tom cruise",
                            "QueryText" : "tom cruise",
                            "QueryParams" : [
                                {
                                    "Name" : "ufn",
                                    "Values" : [
                                        "tom cruise"
                                    ]
                                },
                                {
                                    "Name" : "sid",
                                    "Values" : [
                                        "fdc9ba0e-f198-b3c3-3cad-cf81595a654c"
                                    ]
                                }
                            ],
                            "SnapshotExists" : true
                        },
                        "Description" : {
                            "Kif.Schema" : "Entities.Common.Description[2.8]",
                            "Text" : "Tom Cruise is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders, his first leading role was in the romantic comedy Risky Business, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood, Cruise starred in several more successful films in the 1980s, including the dramas The Color of Money, Cocktail, Rain Man, and Born on the Fourth of July.",
                            "SeeMoreUrl" : {
                                "Kif.Schema" : "Entities.Common.Uri[2.6]",
                                "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise"
                            },
                            "Provider" : {
                                "Name" : "Wikipedia",
                                "Url" : {
                                    "Url" : "http://en.wikipedia.org/wiki/Tom_Cruise",
                                    "DisplayName" : "Wikipedia"
                                }
                            },
                            "FormattedText" : "[[RUBATO]][&Tom Cruise&] is an American actor and filmmaker. Cruise has been nominated for three Academy Awards and has won three Golden Globe Awards. He started his career at age 19 in the 1981 film Endless Love. After portraying supporting roles in Taps and The Outsiders\\, his first leading role was in the romantic comedy Risky Business\\, released in August 1983. Cruise became a full-fledged movie star after starring as Pete \"Maverick\" Mitchell in the action drama Top Gun. One of the biggest movie stars in Hollywood\\, Cruise starred in several more successful films in the 1980s\\, including the dramas The Color of Money\\, Cocktail\\, Rain Man\\, and Born on the Fourth of July."
                        },
                        "DominantType" : "American Actor",
                        "RelatedScalars" : [
                            {
                                "Kif.Schema" : "Entities.Scalar.ScalarPrice[2.6]",
                                "Date" : {
                                    "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                                    "Year" : 2016
                                },
                                "Value" : 470000000,
                                "NumericType" : 2,
                                "Prefix" : 1,
                                "Unit" : 1
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "fb:key/m/07r1h"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.Uri[2.7]",
                                "Url" : "http://www.tomcruise.com/",
                                "DisplayName" : "tomcruise.com"
                            },
                            {
                                "Kif.Schema" : "Entities.Scalar.StringScalar[2.6]",
                                "Value" : "k:wikipedia/Tom_Cruise"
                            }
                        ],
                        "DateOfBirth" : {
                            "Kif.Schema" : "Entities.Common.DateTime[2.7]",
                            "Year" : 1962,
                            "Month" : 7,
                            "Day" : 3
                        },
                        "Professions" : [
                            {
                                "Name" : "Actor"
                            },
                            {
                                "Name" : "Screenwriter"
                            },
                            {
                                "Name" : "Film Producer"
                            },
                            {
                                "Name" : "Television Director"
                            }
                        ],
                        "PlaceOfBirth" : {
                            "Kif.Schema" : "Entities.Place[2.15]",
                            "SatoriId" : "5232ed96-85b1-2edb-12c6-63e6c597a1de",
                            "Name" : "United States",
                            "Description" : {
                                "Kif.Schema" : "Entities.Common.Description[2.8]",
                                "Text" : ""
                            }
                        }
                    },
                    "SegmentTypes" : [
                        "Actor"
                    ],
                    "SatoriTypes" : [
                        "mso:people.person",
                        "mso:event.agent",
                        "mso:film.actor",
                        "mso:biology.organism",
                        "mso:music.artist",
                        "mso:film.writer",
                        "mso:tv.actor",
                        "mso:tv.director",
                        "mso:award.nominee",
                        "mso:ratings.rated_entity",
                        "mso:film.film_song_performer",
                        "mso:award.winner",
                        "mso:book.subject",
                        "mso:celebrities.celebrity",
                        "mso:fictional_universe.person_in_fiction",
                        "mso:film.producer",
                        "mso:film.story_contributor",
                        "mso:food.diet_follower",
                        "mso:media_common.subject",
                        "mso:medicine.notable_person_with_medical_condition",
                        "mso:organization.founder",
                        "mso:projects.project_participant",
                        "mso:tv.personality",
                        "mso:tv.producer"
                    ],
                    "AppContents" : {
                        "ResultLists" : [
                        ]
                    }
                }
            ]
        }
    ]
}